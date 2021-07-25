from django.db import models

from consoles.models import Console
from main.models import Product


class Genre(models.Model):
    """ A model class to represent Genres from DB """
    name = models.CharField(max_length=255)

    def __str__(self):
        return self.name


class Game(Product):
    """ A model class to represent Game products from DB Inherits from main the Product class """
    console_id = models.ForeignKey(Console, on_delete=models.SET_NULL, null=True)
    genres = models.ManyToManyField(Genre)
    rating = models.IntegerField(default=0)

    url = 'games'


