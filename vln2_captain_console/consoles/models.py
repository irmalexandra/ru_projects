from django.db import models

# Create your models here.
from main.models import Product


class Console(Product):
    """ A model class to represent Consoles from DB inherits from main Product class """
    warranty = models.DateTimeField()
    specifications = models.TextField()

    url = "consoles"

