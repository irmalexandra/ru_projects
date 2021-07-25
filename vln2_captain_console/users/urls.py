from django.contrib.auth.views import LogoutView
from django.urls import path
from django.views.generic import RedirectView

from . import views

urlpatterns = [
    # localhost:8000/users/ <---- path so far
    path('', RedirectView.as_view(url='profile'), name="profile_redirect"),
    path('register', views.register, name='register'),
    path('login', views.user_login, name="login"),
    path('logout', LogoutView.as_view(next_page="/"), name="logout"),
    path('profile', views.profile, name="profile"),
    path('profile/edit', views.update_profile, name="update_profile"),
    path('profile/editpayment', views.update_payment_info, name="update_payment_info"),
    path('profile/editshipping', views.update_shipping_info, name="update_shipping_info"),
]
