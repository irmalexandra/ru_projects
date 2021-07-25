import datetime
from itertools import chain
from django.shortcuts import render
from consoles.models import Console
from games.models import Game
from users.models import Profile, SearchHistory, RecentlyViewedGames, RecentlyViewedConsoles


def index(request):
    """
    Returns a dictionary of set queries
    @param request:
    @return context:
    """
    top_sellers = get_game_by_copies_sold()
    releases = get_game_latest_releases()

    context = {'top_sellers': top_sellers, 'release_date': releases}
    recently_viewed = get_recently_viewed(request)
    context['recently_viewed'] = recently_viewed

    return render(request, 'main/index.html', context)


def search(request):
    """
    Pushes new search instances to the database tied to a profileID
    @param request:
    @return context:
    """
    search_string = request.GET.get('search_field')
    if request.user.is_authenticated:
        profile = Profile.objects.filter(user=request.user).first()
        search_instance = SearchHistory.objects.create(profileID=profile, search=search_string)
        search_instance.save()

    games = Game.objects.filter(name__icontains=search_string)
    consoles = Console.objects.filter(name__icontains=search_string)
    return render(request, 'main/search_results.html', {'games': games,
                                                        'consoles': consoles})


def get_recently_viewed(request):
    """
    Returns a list of all recently viewed products.
    :param request: WSGIRequest
    :return: List
    """
    products = []
    # Checks to see if the user is authenticated, If the user is authenticated,
    # we pull all recently viewed products from the DB
    # If not. We check the request.session for the recent_viewed key.
    if request.user.is_authenticated:
        recent_games = RecentlyViewedGames.objects.filter(profileID_id=request.user.profile.id).order_by('-date')
        recent_consoles = RecentlyViewedConsoles.objects.filter(profileID_id=request.user.profile.id).order_by('-date')
        recent_products = sorted(chain(recent_games, recent_consoles), key=lambda prod: prod.date, reverse=True)
        for product in recent_products:
            if type(product).__name__ == 'RecentlyViewedGames':
                products.append(product.gameID)
            else:
                products.append(product.consoleID)
    else:
        if 'recent_viewed' in request.session:
            for id in request.session['recent_viewed']:
                product = Game.objects.filter(id=id).first()
                # check to determine if the id belongs to a game or a console.
                # And adds it to the product list accordingly
                if product:
                    products.append(product)
                else:
                    product = Console.objects.filter(id=id).first()
                    if product:
                        products.append(product)

    return products


def add_recently_viewed(request, id, game=True):
    """
    Adds a product to recently viewed.
    :param request: WSGIRequest
    :param id: int
    :param game: Bool
    """
    # Adds the product to either the DB or the Request.session based on if he's authenticated or not
    if request.user.is_authenticated:
        if game:
            recent = RecentlyViewedGames.objects.filter(gameID=id, profileID_id=request.user.profile.id).first()
            # Check to see if the user has viewed this game before, and updates the time he viewed it last if so.
            if recent is None:
                RecentlyViewedGames.objects.create(gameID=Game.objects.filter(id=id).first(),
                                                   profileID_id=request.user.profile.id)
            else:
                recent.date = datetime.datetime.now()
                recent.save()
        else:
            recent = RecentlyViewedConsoles.objects.filter(consoleID=id,
                                                           profileID_id=request.user.profile.id).first()
            # Check to see if the user has viewed this console before, and updates the time he viewed it last if so.
            if recent is None:
                RecentlyViewedConsoles.objects.create(consoleID=Console.objects.filter(id=id).first(),
                                                      profileID_id=request.user.profile.id)
            else:
                recent.date = datetime.datetime.now()
                recent.save()
    else:
        # Stores all the recently viewed product ID's in the request.session. For a anonymous user

        # check to see if the key is present in the dict
        if 'recent_viewed' not in request.session:
            request.session['recent_viewed'] = []
        # check to determine if the user has viewed this product before. And update the time he viewed it if true
        if id in request.session['recent_viewed']:
            index1 = request.session['recent_viewed'].index(id)
            request.session['recent_viewed'].insert(0, request.session['recent_viewed'][index1])
            request.session['recent_viewed'].pop(index1 + 1)
        else:
            # check to make sure we dont store to many id's in the
            # session since the recently viewed section only displays 4 products.
            if len(request.session['recent_viewed']) >= 4:
                request.session['recent_viewed'].pop()
            request.session['recent_viewed'].insert(0, id)
        request.session.save()


def get_game_by_copies_sold():
    """
    Returns all a queryset of all games sorted by -copies_sold
    :return: Queryset
    """
    games = Game.objects.all().order_by('-copies_sold')
    return games


def get_game_latest_releases():
    """
    Returns a queryset of all games sorted by -release_date
    :return: Queryset
    """
    games = Game.objects.all().order_by('-release_date')
    return games


def page_not_found(request, exception=None):
    """
    Default 404 error handling
    :param request: WSGIRequest
    :param exception:
    :return:
    """
    return render(request, '404.html')


def internal_server_error(request, exception=None):
    """
    Default 500 error handling
    :param request: WSGIRequest
    :param exception:
    :return:
    """
    return render(request, '500.html')


def bad_request(request, exception=None):
    """
    Default 400 error handling
    :param request: WSGIRequest
    :param exception:
    :return:
    """
    return render(request, '400.html')


def permission_denied(request, exception=None):
    """
    Default 403 error handling
    :param request: WSGIReqeust
    :param exception:
    :return:
    """
    return render(request, '403.html')
