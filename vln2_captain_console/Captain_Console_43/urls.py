"""captain_console URL Configuration

The `urlpatterns` list routes URLs to views. For more information please see:
    https://docs.djangoproject.com/en/3.0/topics/http/urls/
Examples:
Function views
    1. Add an import:  from my_app import views
    2. Add a URL to urlpatterns:  path('', views.home, name='home')
Class-based views
    1. Add an import:  from other_app.views import Home
    2. Add a URL to urlpatterns:  path('', Home.as_view(), name='home')
Including another URLconf
    1. Import the include() function: from django.urls import include, path
    2. Add a URL to urlpatterns:  path('blog/', include('blog.urls'))
"""
from django.contrib import admin
from django.urls import path, include
from django.conf.urls import handler404, handler500, handler400

urlpatterns = [
    path('admin/', admin.site.urls),
    path('', include('main.urls')),
    path('games/', include('games.urls')),
    path('consoles/', include('consoles.urls')),
    path('offers/', include('offers.urls')),
    path('users/', include('users.urls')),
    path('', include('carts.urls')),
]

handler404 = 'main.views.page_not_found'
handler500 = 'main.views.internal_server_error'
handler400 = 'main.views.bad_request'
handler403 = 'main.views.permission_denied'
