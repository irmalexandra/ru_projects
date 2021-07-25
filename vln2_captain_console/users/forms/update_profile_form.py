from django.forms import ModelForm
from users.models import Profile
from django.contrib.auth.models import User
from django import forms
from django_countries.fields import CountryField
from django_countries.widgets import CountrySelectWidget


class UserForm(ModelForm):
    """ The user form """
    first_name = forms.CharField(max_length=30, required=False, help_text='Optional')
    last_name = forms.CharField(max_length=30, required=False, help_text='Optional')

    class Meta:
        model = User
        fields = (
            'email',
            'first_name',
            'last_name'
        )


class ProfileForm(ModelForm):
    """ The Profile form """
    address_1 = forms.CharField(max_length=255, required=False, help_text='Optional', label="Address 1")
    address_2 = forms.CharField(max_length=255, required=False, help_text='Optional', label="Address 2")
    city = forms.CharField(max_length=255, required=False, help_text='Optional', label="City")
    postcode = forms.IntegerField(required=False, help_text='Optional', label="Postcode")
    country = CountryField().formfield(required=False)
    profile_image = forms.CharField(max_length=255, required=False, help_text='Optional', label="Profile Image")

    class Meta:
        model = Profile
        fields = ('address_1', 'address_2', 'city', 'postcode', 'country', 'profile_image')
        widgets = {'country': CountrySelectWidget()}
