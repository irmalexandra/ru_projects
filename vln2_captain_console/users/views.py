from django.contrib.auth import authenticate, login
from django.http import HttpResponse, JsonResponse
from django.shortcuts import render, redirect
from users.models import Profile, SearchHistory
from users.models import ShippingInformation, PaymentInformation
from carts.views import get_order_history
from users.forms.update_profile_form import ProfileForm, UserForm
from users.forms.register_form import RegisterForm
from carts.forms.payment_form import PaymentForm
from carts.forms.shipping_form import ShippingForm
from django.contrib.auth.decorators import login_required
from django.contrib.auth.models import User

INFO_KEY_DICT = {'username': '', 'email': '', 'first_name': '', 'last_name': '', 'address_1': '',
                 'address_2': '', 'city': '', 'postcode': '', 'country': '', 'profile_image': ''}
LABEL_DICT = {'username': 'Username', 'email': 'Email', 'first_name': 'First name', 'last_name': 'Last name',
              'address_1': 'Address 1', 'address_2': 'Address 2', 'city': 'City', 'postcode': 'Postcode',
              'country': 'Country', 'profile_image': 'Profile image', 'card_number': 'Card number',
              'expiration_date': 'Expiration date', 'cvv': 'CVV/CVC'}
EXCLUDED_FIELDS_TPL = ('_state', 'id')


def user_login(request):
    """
    Authenticates a login request and returns a string depending on whether authentication was successful
    @param request: WSGI request
    @return string: HttpResponse
    """
    username = request.GET.get('username')
    password = request.GET.get('pwd')
    user = authenticate(username=username, password=password)
    if user is not None:
        login(request, user)
        return HttpResponse('loggedin')
    else:
        return HttpResponse('badcredentials')


def register(request):
    """
    Creates a registration form from a POST request
    @param request: WSGI request
    @return string: HttpResponse
    @return json object: JsonResponse
    """
    form = RegisterForm(request.POST)

    if form.is_valid():
        instance = form.save()
        username = request.POST.get('username')
        password = request.POST.get('password1')
        user = authenticate(username=username, password=password)
        login(request, user)
        current_profile = Profile.objects.filter(user=request.user).first()

        if current_profile == None:
            current_profile = Profile(user_id=instance.id)
            current_profile.save()
        return HttpResponse('loggedin')
    else:
        return JsonResponse(form.errors)


@login_required
def profile(request):
    """
    pulls various information tied to the current user from the database and returns it as
    various dicts, lists and queries
    @param request: WSGI request
    @return render: HttpResponse
    """
    user = User.objects.filter(username=request.user.username).first()
    current_profile = Profile.objects.filter(user=request.user).first()
    profile_dict = current_profile.__dict__
    user_dict = user.__dict__

    for key in INFO_KEY_DICT:
        if key in profile_dict:
            INFO_KEY_DICT[key] = profile_dict[key]
        elif key in user_dict:
            INFO_KEY_DICT[key] = user_dict[key]

    complete_info_dict = dict((LABEL_DICT[key], value) for (key, value) in INFO_KEY_DICT.items())  # creates a merged
    # dictionary using the values from LABEL_DICT as keys and the values from INFO_KEY_DICT as values
    searches = SearchHistory.objects.filter(profileID=current_profile.id).order_by('-id').all()
    order_list = get_order_history(request)

    user_payment_info = current_profile.payment_information_id
    if user_payment_info is None:
        user_payment_info = PaymentInformation()

    user_shipping_info = current_profile.shipping_information_id
    if user_shipping_info is None:
        user_shipping_info = ShippingInformation()

    user_payment_dict = user_payment_info.__dict__
    user_shipping_dict = user_shipping_info.__dict__

    for key in EXCLUDED_FIELDS_TPL:  # strips unwanted fields from the dictionaries
        del user_payment_dict[key]
        del user_shipping_dict[key]

    complete_payment_dict = dict((LABEL_DICT[key], value) for (key, value) in user_payment_dict.items())
    complete_shipping_dict = dict((LABEL_DICT[key], value) for (key, value) in user_shipping_dict.items())

    return render(request, 'users/profile.html', {
        'profile_info_dict': complete_info_dict,
        'payment_info_dict': complete_payment_dict,
        'shipping_info_dict': complete_shipping_dict,
        'searches': searches,
        'order_history': order_list
    })


@login_required
def update_profile(request):
    """
    Sends forms based on user and profile information from the database
    @param request: WSGI request
    @return render: HttpResponse
    """
    current_profile = Profile.objects.filter(user=request.user).first()

    if request.method == 'POST':
        profile_form = ProfileForm(instance=current_profile, data=request.POST)
        user_form = UserForm(instance=current_profile.user, data=request.POST)

        if profile_form.is_valid() and user_form.is_valid():
            profile_form.save()
            user_form.save()
            return redirect('profile')

    return render(request, 'users/update_profile.html', {
        'profile_form': ProfileForm(instance=current_profile),
        'user_form': UserForm(instance=current_profile.user, auto_id='False')
    })


@login_required
def update_payment_info(request):
    """
    Sends a form based on user payment information from the database
    @param request: WSGI request
    @return render: HttpRespone
    """
    current_profile = Profile.objects.filter(user=request.user).first()
    user_payment_info = current_profile.payment_information_id

    if user_payment_info is None:
        user_payment_info = PaymentInformation()

    if request.method == 'POST':
        payment_form = PaymentForm(instance=user_payment_info, data=request.POST)

        if payment_form.is_valid():
            current_profile.payment_information_id = payment_form.save()
            current_profile.save()
            return redirect('profile')
        payment_form = PaymentForm(instance=user_payment_info, data=request.POST)
    else:
        payment_form = PaymentForm(instance=user_payment_info)

    return render(request, 'users/update_payment_info.html', {
        'payment_form': payment_form
    })


@login_required
def update_shipping_info(request):
    """
    Sends a form based on user shipping information from the database
    @param request: WSGI Request
    @return render: HttpResponse
    """
    current_profile = Profile.objects.filter(user=request.user).first()
    user_shipping_info = current_profile.shipping_information_id

    if user_shipping_info is None:
        user_shipping_info = ShippingInformation()

    if request.method == 'POST':
        shipping_form = ShippingForm(instance=user_shipping_info, data=request.POST)

        if shipping_form.is_valid():
            current_profile.shipping_information_id = shipping_form.save()
            current_profile.save()
            return redirect('profile')
        shipping_form = ShippingForm(instance=user_shipping_info, data=request.POST)
    else:
        shipping_form = ShippingForm(instance=user_shipping_info)

    return render(request, 'users/update_shipping_info.html', {
        'shipping_form': shipping_form
    })
