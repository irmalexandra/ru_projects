from django.urls import path
from . import views
urlpatterns = [
    # localhost:8000/   <---- Path so far
    path('', views.index, name="main-index"),
    path('search/', views.search, name="search-results"),
]