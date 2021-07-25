from django.urls import path
from . import views
urlpatterns = [
    # localhost:8000/offers/  <---- Path so far
    path('', views.index, name="offers-index"),
]