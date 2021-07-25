from django import forms
from django.contrib.auth.forms import AuthenticationForm
from django.contrib.auth.models import User


class LoginForm(AuthenticationForm):
    """ The login form """
    username = forms.CharField(max_length=254, required=True)
    password = forms.CharField(label="Password", widget=forms.PasswordInput, required=True)

    class Meta:
        model = User
        fields = ('username', 'password')
