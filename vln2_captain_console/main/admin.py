from django.contrib import admin

# Register your models here.
from main.models import Manufacturer, ExtraImages

admin.site.register(Manufacturer)
admin.site.register(ExtraImages)

