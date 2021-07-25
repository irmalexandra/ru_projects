from django.db import models
from main.models import Product
from users.models import Cart


class CartItems(models.Model):
    productID = models.ForeignKey(Product, on_delete=models.PROTECT)
    quantity = models.IntegerField()
    price = models.IntegerField()
    cartID = models.ForeignKey(Cart, on_delete=models.CASCADE)
    total_price = models.IntegerField()



