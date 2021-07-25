from django.urls import path
from . import views
urlpatterns = [
    # localhost:8000/consoles/  <--- Path so far
    path('', views.console_default_view, name="console_default_view"),
    # path('name=A_Z', views.index_ascending, name="console_index_ascending"),
    # path('name=Z_A', views.index_descending, name="console_index_descending"),
    path('<int:id>', views.get_console_by_id, name="console_details"),
    # path('test', views.get_console_by_copies_sold),
    # path('price=low_to_high', views.sort_by_price_ascending, name="console_price_ascending"),
    # path('price=high_to_low', views.sort_by_price_descending, name="console_price_descending")
    path('sort/<int:id>', views.console_sort_view, name='console_sort_view'),
]


