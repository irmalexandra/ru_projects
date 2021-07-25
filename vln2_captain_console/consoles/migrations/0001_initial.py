# Generated by Django 3.0.5 on 2020-05-12 22:07

from django.db import migrations, models
import django.db.models.deletion


class Migration(migrations.Migration):

    initial = True

    dependencies = [
        ('main', '0001_initial'),
    ]

    operations = [
        migrations.CreateModel(
            name='Console',
            fields=[
                ('product_ptr', models.OneToOneField(auto_created=True, on_delete=django.db.models.deletion.CASCADE, parent_link=True, primary_key=True, serialize=False, to='main.Product')),
                ('warranty', models.DateTimeField()),
                ('specifications', models.TextField()),
            ],
            bases=('main.product',),
        ),
    ]