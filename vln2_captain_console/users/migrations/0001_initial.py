# Generated by Django 3.0.5 on 2020-05-12 22:07

from django.conf import settings
from django.db import migrations, models
import django.db.models.deletion
import django_countries.fields


class Migration(migrations.Migration):

    initial = True

    dependencies = [
        ('carts', '0001_initial'),
        ('consoles', '0001_initial'),
        ('games', '0001_initial'),
        migrations.swappable_dependency(settings.AUTH_USER_MODEL),
        ('main', '0001_initial'),
    ]

    operations = [
        migrations.CreateModel(
            name='Profile',
            fields=[
                ('id', models.AutoField(auto_created=True, primary_key=True, serialize=False, verbose_name='ID')),
                ('address_1', models.CharField(max_length=255, null=True)),
                ('address_2', models.CharField(max_length=255, null=True)),
                ('city', models.CharField(max_length=255, null=True)),
                ('postcode', models.IntegerField(null=True)),
                ('country', django_countries.fields.CountryField(max_length=2, null=True)),
                ('profile_image', models.CharField(max_length=999, null=True)),
                ('payment_information_id', models.ForeignKey(null=True, on_delete=django.db.models.deletion.SET_NULL, to='carts.PaymentInformation')),
                ('shipping_information_id', models.ForeignKey(null=True, on_delete=django.db.models.deletion.SET_NULL, to='carts.ShippingInformation')),
                ('user', models.OneToOneField(on_delete=django.db.models.deletion.CASCADE, to=settings.AUTH_USER_MODEL)),
            ],
        ),
        migrations.CreateModel(
            name='SearchHistory',
            fields=[
                ('id', models.AutoField(auto_created=True, primary_key=True, serialize=False, verbose_name='ID')),
                ('search', models.TextField()),
                ('profileID', models.ForeignKey(on_delete=django.db.models.deletion.CASCADE, to='users.Profile')),
            ],
        ),
        migrations.CreateModel(
            name='Review',
            fields=[
                ('id', models.AutoField(auto_created=True, primary_key=True, serialize=False, verbose_name='ID')),
                ('recommend', models.BooleanField(default=True)),
                ('feedback', models.CharField(max_length=999)),
                ('datetime', models.DateTimeField(auto_now_add=True)),
                ('gameID', models.ForeignKey(null=True, on_delete=django.db.models.deletion.SET_NULL, to='main.Product')),
                ('profileID', models.ForeignKey(null=True, on_delete=django.db.models.deletion.SET_NULL, to='users.Profile')),
            ],
        ),
        migrations.CreateModel(
            name='RecentlyViewedGames',
            fields=[
                ('id', models.AutoField(auto_created=True, primary_key=True, serialize=False, verbose_name='ID')),
                ('date', models.DateTimeField(auto_now_add=True)),
                ('gameID', models.ForeignKey(null=True, on_delete=django.db.models.deletion.CASCADE, to='games.Game')),
                ('profileID', models.ForeignKey(null=True, on_delete=django.db.models.deletion.CASCADE, to='users.Profile')),
            ],
        ),
        migrations.CreateModel(
            name='RecentlyViewedConsoles',
            fields=[
                ('id', models.AutoField(auto_created=True, primary_key=True, serialize=False, verbose_name='ID')),
                ('date', models.DateTimeField(auto_now_add=True)),
                ('consoleID', models.ForeignKey(null=True, on_delete=django.db.models.deletion.CASCADE, to='consoles.Console')),
                ('profileID', models.ForeignKey(null=True, on_delete=django.db.models.deletion.CASCADE, to='users.Profile')),
            ],
        ),
        migrations.CreateModel(
            name='OrderHistory',
            fields=[
                ('id', models.AutoField(auto_created=True, primary_key=True, serialize=False, verbose_name='ID')),
                ('orderId', models.ForeignKey(on_delete=django.db.models.deletion.CASCADE, to='carts.Order')),
                ('profileID', models.ForeignKey(on_delete=django.db.models.deletion.CASCADE, to='users.Profile')),
            ],
        ),
        migrations.CreateModel(
            name='GameReview',
            fields=[
                ('id', models.AutoField(auto_created=True, primary_key=True, serialize=False, verbose_name='ID')),
                ('gameID', models.ForeignKey(on_delete=django.db.models.deletion.CASCADE, to='games.Game')),
                ('reviewID', models.ForeignKey(on_delete=django.db.models.deletion.CASCADE, to='users.Review')),
            ],
        ),
    ]
