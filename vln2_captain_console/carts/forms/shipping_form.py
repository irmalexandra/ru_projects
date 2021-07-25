from django import forms
from django.forms import ModelForm
from users.models import ShippingInformation
from django_countries.fields import CountryField
from django_countries.widgets import CountrySelectWidget


class ShippingForm(ModelForm):
    """ The shipping information form """
    first_name = forms.CharField(max_length=30, label='First Name')
    last_name = forms.CharField(max_length=30, label='Last Name')
    address_1 = forms.CharField(max_length=255, label='Address 1')
    address_2 = forms.CharField(max_length=255, required=False, help_text='Optional', label='Address 2')
    city = forms.CharField(max_length=255, label='City')
    postcode = forms.IntegerField(label='Postcode')
    country = CountryField().formfield(required=False)

    class Meta:
        model = ShippingInformation
        fields = ('first_name', 'last_name', 'address_1', 'address_2', 'city', 'postcode', 'country')
        widgets = {'country': CountrySelectWidget()}
