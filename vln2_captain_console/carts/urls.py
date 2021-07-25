from django.urls import path
from . import views

urlpatterns = [
    path('carts/add', views.cart_add, name='cart_add'),
    path('remove_product', views.remove_product, name='remove_product'),
    path('clear_cart', views.clear_cart, name='clear_cart'),
    path('carts/', views.index, name="cart-index"),
    path('update_cart_items', views.update_cart_items),
    path('carts/shipping_information', views.input_shipping_info, name='shipping_info'),
    path('carts/shipping_information/<int:old_id>', views.input_shipping_info, name='shipping_info'),
    path('carts/payment_information/<int:shipping_id>/', views.input_payment_info, name='payment_info'),
    path('carts/payment_information/<int:shipping_id>/<int:old_id>', views.input_payment_info, name='payment_info'),
    path('carts/overview/<int:shipping_id>/<int:payment_id>', views.overview, name='overview')

]
