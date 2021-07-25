from django.http import HttpResponse
from django.shortcuts import render, redirect
from carts.forms.payment_form import PaymentForm
from carts.forms.shipping_form import ShippingForm
from main.models import Product
from carts.models import Cart, CartItems
from users.models import Profile, PaymentInformation, ShippingInformation, Order


def index(request):
    """
    handles the main cart path.
    sends all required info to the html
    :param request: WSGIRequest
    :return: Render http response
    """
    context = get_cart_info(request) # Gets the dicts to be used by the html to display the cart via django
    return render(request, 'carts/index.html', context)


def update_cart_items(request):
    """
    Updates the quantity of the item corresponding to the id given through the AJAX request

    :param request: WSGIRequest
    :return: Http response
    """
    if request.is_ajax():
        item_id = int(request.POST.get('id'))  # ID of the product
        item_quantity = int(request.POST.get('quantity'))

        if request.user.is_authenticated:

            profile = Profile.objects.filter(user=request.user).first()
            user_cart = Cart.objects.filter(profileID=profile, check_out=False).first()
            user_cart_items = CartItems.objects.filter(cartID=user_cart.id, productID=item_id).first()
            user_cart_items.quantity = item_quantity
            user_cart_items.total_price = user_cart_items.price * user_cart_items.quantity
            user_cart_items.save()

        else:
            for x in range(len(request.session['cart'])):
                if request.session['cart'][x]['id'] == item_id:
                    request.session['cart'][x]['quantity'] = item_quantity
                    request.session['cart'][x]['total_price'] = int(request.session['cart'][x]['price']) * item_quantity

        request.session.save()
        return HttpResponse("success")


def input_shipping_info(request, old_id = None):
    """
    handles the shipping information html.
    Auto fills shipping info based on if the user is signed in or not
    creates a shipping info class instance from a posted form
    and pushes it into the DB, then redirects to payment info page

    :param request: WSGIRequest
    :return: Render http response
    :return: Redirect http response
    """
    shipping_form = None
    context = get_cart_info(request)
    if old_id:  # If an already saved form is to be used
        shipping_info = ShippingInformation.objects.filter(id=old_id).first()
        shipping_form = ShippingForm(instance=shipping_info)

    if request.method == "POST":  # if the form is submitted
        shipping_form = ShippingForm(data=request.POST)
        if shipping_form.is_valid():
            the_id = None  # also handles duplicate
            all_shipping_info = ShippingInformation.objects.all()
            for info in all_shipping_info:
                if info.last_name == request.POST['last_name'] and \
                        info.first_name == request.POST['first_name'] and \
                        info.city == request.POST['city'] and \
                        info.postcode == int(request.POST['postcode']) and \
                        info.address_2 == request.POST['address_2'] and \
                        info.address_1 == request.POST['address_1'] and \
                        info.country == request.POST['country']:
                    the_id = info.id  # An id was found, thus info is a duplicate
                    break

            if the_id == None:  # <----- if NOT duplicate
                form_instance = shipping_form.save()
                the_id = form_instance.id
            return redirect('payment_info', the_id)

    if shipping_form:  # If the shipping form did not make it through validation
        context['shipping_info_form'] = shipping_form
    else:
        # gets the shipping info from the account if it has any
        if request.user.is_authenticated:
            profile = Profile.objects.filter(user=request.user).first()
            if profile.shipping_information_id:
                shipping_info = ShippingInformation.objects.filter(id=profile.shipping_information_id.id).first()
            else:
                shipping_info = ShippingInformation()
        else:
            # Anonymous user handling
            shipping_info = ShippingInformation()

        context['shipping_info_form'] = ShippingForm(instance=shipping_info)
    return render(request, 'carts/shipping_info.html', context)


def input_payment_info(request, shipping_id, old_id = None):
    """
    handles the payment information html.
    Auto fills payment info based on if the user is signed in or not
    creates a payment info class instance from a posted form
    and pushes it into the DB, then redirects to overview info page

    :param request: WSGIRequest
    :return: Render http response
    :return: Redirect http response
    """

    payment_form = None

    if old_id:  # If an already saved form is to be used
        payment_info = PaymentInformation.objects.filter(id=old_id).first()
        payment_form = PaymentForm(instance=payment_info)

    if request.method == "POST":  # if the form is submitted
        payment_form = PaymentForm(data=request.POST)
        if payment_form.is_valid():
            dupe = None  # duplicate flag
            all_payment_info = PaymentInformation.objects.all()
            for info in all_payment_info:
                expiration_date = str(info.expiration_date)
                expiration_fixed = expiration_date[5:7] + "/" + expiration_date[2:4]
                if info.last_name == request.POST['last_name'] and \
                        info.first_name == request.POST['first_name'] and \
                        int(info.card_number) == int(request.POST['card_number']) and \
                        expiration_fixed == request.POST['expiration_date'] and \
                        info.cvv == request.POST['cvv']:
                    dupe = True  # Duplicate found
                    payment_instance = PaymentInformation.objects.filter(id=info.id).first()
                    break

            if dupe == None:  # <----- if NOT duplicate
                payment_instance = payment_form.save()
            return redirect('overview', shipping_id=shipping_id, payment_id=payment_instance.id)

    context = get_cart_info(request)  # Gets the dicts to be used by the html to display the cart via django
    if payment_form:  # If the payment form did not make it through validation
        context['payment_info_form'] = payment_form
    else:
        # gets the payment info from the account if it has any
        if request.user.is_authenticated:
            profile = Profile.objects.filter(user=request.user).first()
            if profile.payment_information_id:
                payment_info = PaymentInformation.objects.filter(id=profile.payment_information_id.id).first()
            else:
                payment_info = PaymentInformation()
        else:
            # Anonymous user handling
            payment_info = PaymentInformation()
        context['payment_info_form'] = PaymentForm(instance=payment_info)

    return render(request, 'carts/payment_info.html', context)


def overview(request, shipping_id, payment_id):
    """
    handles the shipping information html.
    Auto fills shipping info based on if the user is signed in or not
    creates a shipping info class instance from a posted form
    and pushes it into the DB, then redirects to payment info page

    :param request: WSGIRequest
    :param shipping_id: int
    :param request: WSGIRequest
    :return: Render http response
    :return: Redirect http response
    """
    context = get_cart_info(request)  # Gets the dicts to be used by the html to display the cart via django
    total_price = 0

    if request.method == 'POST':
        #  redirects to overview path, pushes all gathered info into corresponding DB tables
        shipping_instance = ShippingInformation.objects.filter(id=shipping_id).first()
        payment_instance = PaymentInformation.objects.filter(id=payment_id).first()
        if request.user.is_authenticated:
            profile = Profile.objects.filter(user=request.user).first()
            cart = Cart.objects.filter(profileID=profile, check_out=False).first()
            items = CartItems.objects.filter(cartID=cart.id)
            for item in items:
                total_price += item.total_price
                product = Product.objects.filter(id=item.productID.id).first()
                product.copies_sold += item.quantity
                product.quantity -= item.quantity
                product.save()

            Cart.objects.create(profileID=profile, check_out=False)
            cart.check_out = True

        else:
            #  Anonymous handling
            cart = Cart.objects.create(profileID=None, check_out=True)
            for item in request.session['cart']:
                total_price += int(item['total_price'])
                product = Product.objects.filter(id=int(item['id'])).first()
                product.quantity -= int(item['quantity'])
                product.copies_sold += int(item['quantity'])
                product.save()
                CartItems.objects.create(productID=Product.objects.filter(id=item['id']).first(),
                                         quantity=item['quantity'],
                                         price=item['price'],
                                         cartID=cart,
                                         total_price=int(item['total_price']))
            request.session['cart'] = []

        order = Order.objects.create(
            shipping_information_id=shipping_instance,
            payment_information_id=payment_instance,
            cartID=cart,
            total_price=total_price
        )

        cart.save()
        order.save()
        context['order_complete'] = True

    # info gathered over the process
    payment_info = PaymentInformation.objects.filter(id=payment_id).first()
    shipping_info = ShippingInformation.objects.filter(id=shipping_id).first()

    context['payment_info'] = payment_info
    context['shipping_info'] = shipping_info

    return render(request, 'carts/overview.html', context)


def cart_add(request):
    """
    Adds a product into the cart via an ajax request.

    :param request: WSGIRequest
    :return: Http response
    """
    if request.is_ajax():
        returned_quantity = 1  # a response that tells javaScript how many of the product are in the cart
        product_id = int(request.POST.get('id'))
        product = Product.objects.filter(id=product_id).first()

        if request.user.is_authenticated:
            profile = Profile.objects.filter(user=request.user).first()
            user_cart = Cart.objects.filter(profileID=profile, check_out=False).first()
            if not user_cart:
                user_cart = Cart.objects.create(profileID=profile, check_out=False)
            cart_items = CartItems.objects.filter(cartID=user_cart.id, productID=product.id)
            if len(cart_items) != 0:
                # If the product is already in the cart
                first_item = cart_items.first()  # the duplicate product in the cart
                first_item.quantity += 1
                returned_quantity = first_item.quantity
                first_item.total_price = first_item.quantity * first_item.price
                first_item.save()
            else:
                # If the product is not in the cart
                if product.on_sale:
                    price = product.discount_price
                else:
                    price = product.price
                CartItems.objects.create(productID=product,
                                         quantity=1,
                                         cartID=user_cart,
                                         price=price,
                                         total_price=price*1)

        else:
            #  Anonymous handling
            duplicate = False  # duplicate flag
            if 'cart' not in request.session.keys():
                request.session['cart'] = []
            for x in range(len(request.session['cart'])):
                if request.session['cart'][x]['id'] == product.id:
                    request.session['cart'][x]['quantity'] += 1
                    request.session['cart'][x]['total_price'] += request.session['cart'][x]['price']
                    returned_quantity = request.session['cart'][x]['quantity']
                    duplicate = True

            if not duplicate:
                if product.on_sale:
                    price = product.discount_price
                else:
                    price = product.price

                request.session['cart'].append({
                    'name': product.name,
                    'price': price,
                    'img': product.product_display_image,
                    'quantity': 1,
                    'id': product.id,
                    'total_price': price,
                    'discount': product.discount
                })
            request.session.save()
        return HttpResponse(returned_quantity)


def get_order_history(request):
    """
    Returns a list of the current users order history
    :param request: WSGIRequest
    :return: list
    """
    profile_id = request.user.profile.id
    order_history_list = []
    carts = Cart.objects.filter(profileID=profile_id, check_out=True)  # all checked out carts, as in orders
    if carts:
        for cart in carts:
            order_dict = {'order': Order.objects.filter(cartID=cart.id).first(),
                          'cart_items': CartItems.objects.filter(cartID=cart.id)}
            order_history_list.append(order_dict)

        return order_history_list


def remove_product(request):
    """
    Removes a product from the users cart
    via Ajax request
    :param request: WSGIRequest
    :return: Http response
    """
    if request.is_ajax():
        product_id = int(request.POST.get("id"))
        if request.user.is_authenticated:
            profile = Profile.objects.filter(user=request.user).first()
            user_cart_id = Cart.objects.filter(profileID=profile).first().id
            product = CartItems.objects.filter(cartID=user_cart_id, productID=product_id).first()
            product.delete()
        else:
            # Anonymous handling
            for x in range(len(request.session['cart'])):
                if request.session['cart'][x]['id'] == product_id:
                    request.session['cart'].pop(x)
                    break
            request.session.save()

        return HttpResponse("success")


def clear_cart(request):
    """
    Clears a the users cart via an Ajax request
    :param request: WSGIRequest
    :return: Http response
    """
    if request.is_ajax():
        if request.user.is_authenticated:
            profile = Profile.objects.filter(user=request.user).first()
            user_cart = Cart.objects.filter(profileID=profile).first()
            user_cart.delete()
        else:
            # Anonymous handling
            request.session.clear()
            request.session.save()

        return HttpResponse("success")


def get_cart_info(request):
    """
    Gets the users cart info and returns it for html use
    :param request: WSGIRequest
    :return: dict
    """
    class Model:
        # the form of which is needed for the products
        name = ""
        quantity = 0
        img = ""
        price = 0
        id = 0
        total_price = 0
        discount = 0

    models = []
    price_sum = 0
    if request.user.is_authenticated:
        profile = Profile.objects.filter(user=request.user).first()
        user_cart = Cart.objects.filter(profileID=profile, check_out=False).first()
        if user_cart:
            cart = CartItems.objects.filter(cartID=user_cart.id)
            for product in cart:
                model = Model()
                model.quantity = product.quantity
                model.price = product.price * product.quantity
                model.id = product.productID
                item = Product.objects.filter(id=product.productID.id).first()
                price_sum += product.price * product.quantity
                model.img = item.product_display_image
                model.name = item.name
                model.discount = product.productID.discount
                model.total_price = product.price * product.quantity
                models.append(model)

    else:
        # Anonymous handling
        if "cart" in request.session.keys():
            for product in request.session['cart']:
                model = Model()
                model.quantity = product['quantity']
                model.price = product['price'] * product['quantity']
                price_sum += product['price'] * product['quantity']
                model.img = product['img']
                model.name = product['name']
                model.id = product['id']
                model.discount = product['discount']
                model.total_price = int(product['quantity']) * int(product['price'])
                models.append(model)

    context = {
        'price_sum': price_sum,
        'products': models
    }

    return context
