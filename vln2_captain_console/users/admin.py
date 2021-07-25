from django.contrib.auth.models import User

from .models import Profile

from django.contrib import admin

# Register your models here.

admin.site.register(Profile)
