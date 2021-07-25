from django.db import models
from django_countries.fields import CountryField
from consoles.models import Console
from games.models import Game
from django.contrib.auth.models import User
from django.utils.timezone import now
from creditcards.models import CardExpiryField, CardNumberField, SecurityCodeField
from main.models import Product


class PaymentInformation(models.Model):
    """ A model class to represent Payment Information from DB """
    first_name = models.CharField(max_length=255)
    last_name = models.CharField(max_length=255)
    card_number = CardNumberField()
    expiration_date = CardExpiryField()
    cvv = SecurityCodeField()

    def __str__(self):
        return self.first_name + " " + self.last_name


class ShippingInformation(models.Model):
    """ A model class to represent Shipping information from DB """
    first_name = models.CharField(max_length=255)
    last_name = models.CharField(max_length=255)
    address_1 = models.CharField(max_length=255)
    address_2 = models.CharField(max_length=255)
    city = models.CharField(max_length=255)
    postcode = models.IntegerField()
    country = CountryField()

    def __str__(self):
        return "Shipping information: " + self.first_name + " " + self.last_name


class Profile(models.Model):
    """ A model class to represent Profile from DB it extends one to one with the Auth_Users default from Django """
    user = models.OneToOneField(User, on_delete=models.CASCADE)
    address_1 = models.CharField(max_length=255, null=True)
    address_2 = models.CharField(max_length=255, null=True)
    city = models.CharField(max_length=255, null=True)
    postcode = models.IntegerField(null=True)
    country = CountryField(null=True)
    profile_image = models.CharField(max_length=999, null=True)
    payment_information_id = models.ForeignKey(PaymentInformation, on_delete=models.SET_NULL, null=True)
    shipping_information_id = models.ForeignKey(ShippingInformation, on_delete=models.SET_NULL, null=True)

    def __str__(self):
        return "username: " + self.user.username


class Review(models.Model):
    """ A class to store a game review """
    profileID = models.ForeignKey(Profile, on_delete=models.SET_NULL, null=True)
    gameID = models.ForeignKey(Product, on_delete=models.SET_NULL, null=True)
    recommend = models.BooleanField(default=True)
    feedback = models.CharField(max_length=999)
    datetime = models.DateTimeField(auto_now_add=True, blank=True)


class GameReview(models.Model):
    """ A class to store all reveiws for a game """
    gameID = models.ForeignKey(Game, on_delete=models.CASCADE)
    reviewID = models.ForeignKey(Review, on_delete=models.CASCADE)


class Cart(models.Model):
    """ A model class to represent Cart from DB """
    profileID = models.ForeignKey(Profile, on_delete=models.PROTECT, null=True)
    check_out = models.BooleanField()
    date_created = models.DateTimeField(default=now)


class Order(models.Model):
    """ A model class to represent Order from DB """
    cartID = models.ForeignKey(Cart, on_delete=models.PROTECT)
    datetime = models.DateTimeField(default=now)
    shipping_information_id = models.ForeignKey(ShippingInformation, on_delete=models.PROTECT)
    payment_information_id = models.ForeignKey(PaymentInformation, on_delete=models.PROTECT)
    total_price = models.IntegerField(default=0, null=True)


class SearchHistory(models.Model):
    """ Stores the search history for a profile,
    the search variable is the string the user searched for in the search engine """
    search = models.TextField()
    profileID = models.ForeignKey(Profile, on_delete=models.CASCADE)


class RecentlyViewedGames(models.Model):
    """ All the recently viewed Games a user as viewed """
    date = models.DateTimeField(auto_now_add=True, blank=True)
    gameID = models.ForeignKey(Game, on_delete=models.CASCADE, null=True)
    profileID = models.ForeignKey(Profile, on_delete=models.CASCADE, null=True)


class RecentlyViewedConsoles(models.Model):
    """ All the recently viewed Console a user as viewed """
    date = models.DateTimeField(auto_now_add=True, blank=True)
    consoleID = models.ForeignKey(Console, on_delete=models.CASCADE, null=True)
    profileID = models.ForeignKey(Profile, on_delete=models.CASCADE, null=True)
