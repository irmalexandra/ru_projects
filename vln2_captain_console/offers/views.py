from django.shortcuts import render


# Create your views here.
from games import views as game_views
from consoles import views as console_views


def index(request):
    """
    Sets the context for the offers default view and returns Render
    :param request: WSGIRequest
    :return Render: HttpResponse
    """
    game_offers = game_views.get_game_offers(request)
    console_offers = console_views.get_console_offers(request)
    context = {"offers_tab": "active", 'game_offers': game_offers, 'console_offers': console_offers}
    print(context)
    return render(request, "offers/index.html", context)
