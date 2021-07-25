from django.urls import path
from . import views

urlpatterns = [
    # localhost:8000/games/  <--- Path so far
    path('', views.game_default_view, name="default_view"),
    path('<int:id>', views.get_game_by_id, name="game_details"),
    path('add_review', views.add_review, name="add_review"),
    path('sort/<int:id>', views.game_sort_view, name='game_sort_view'),
    path('filter_by_genre/<int:id>', views.genre_filter_view, name='filter_by_genre'),
    path('filter_by_genre/<str:id>', views.genre_filter_view, name='filter_by_genre'),
    path('filter_by_console/<int:id>', views.console_filter_view, name='filter_by_console'),
    path('filter_by_console/<str:id>', views.console_filter_view, name='filter_by_console'),
]
