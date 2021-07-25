from django import forms
from carts.models import CartItems


class CartItemsForm(forms.ModelForm):
    """ A form to represent the item in the cart """
    quantity = forms.IntegerField()
    productID = forms.IntegerField()
    price = forms.IntegerField()
    cartID = forms.IntegerField()

    class Meta:
        model = CartItems
        fields = (
            'quantity',
            'productID',
            'price',
            'cartID',
        )
