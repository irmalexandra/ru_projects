from django.contrib.auth.decorators import login_required
from django.http import HttpResponse
from django.shortcuts import render, get_object_or_404
import datetime
from consoles.models import Console
from games.models import Game, Genre
from main.views import add_recently_viewed
from users.models import Profile, Review

# Create your views here.
SORT_DICT = {
    1: 'name',
    2: '-name',
    3: 'price',
    4: '-price',
    5: 'copies_sold',
    6: '-copies_sold',
    7: '-release_date',
    8: 'release_date',
    9: '-rating',
    10: 'rating'
}
SORT_LABELS = {
    1: 'A-Z',
    2: 'Z-A',
    3: 'Price: Low to High',
    4: 'Price: High to Low',
    5: 'Least Popular',
    6: 'Top Sellers',
    7: 'Latest',
    8: 'Oldest',
    9: 'Rating: Highest-Lowest',
    10: 'Rating: Lowest-Highest'
}


def genre_filter_view(request, id):
    """
    Calls the filter_sorter with the genre id
    :param request: WSGIRequest
    :param id: int
    :return filter_sorter:HttpResponse
    """
    return filter_sorter(request, id)


def console_filter_view(request, id):
    """
    Calls the filter_sorter with the console id
    :param request: WSGIRequest
    :param id: int
    :return filter_sorter:HttpResponse
    """
    return filter_sorter(request, None, id)


def game_sort_view(request, id):
    """
    Calls the filter_sorter with the sort id
    :param request: WSGIRequest
    :param id: int
    :return filter_sorter:HttpResponse
    """
    return filter_sorter(request, None, None, id)


def game_default_view(request):
    """
    Calls the filter_sorter with the default values
    and resets the session, turning off all filtering and sorting
    :param request: WSGIRequest
    :return filter_sorter:HttpResponse
    """
    request.session.pop('genre', None)
    request.session.pop('console', None)
    request.session.pop('sort', None)
    return filter_sorter(request)


def filter_sorter(request, genre_id=None, console_id=None, sort=None):
    """
    Sets the context for the Render based on optional parameters. Sorts and filters
    the game queryset before adding it to the context
    :param request: WSGIRequest
    :param genre_id: int, str
    :param console_id: int, str
    :param sort: int
    :return Render:HttpResponse
    """
    context = {"games_tab": "active",
               'genres': Genre.objects.all().order_by('name'),
               'consoles': Console.objects.all().order_by('name'),
               'sort_dict': SORT_LABELS}

    # Checks to determine which id was given, checks to see if the id is int or str.
    # If the value is not a int it pops the value from the dictionary, return the filtering to default
    if genre_id:
        if type(genre_id).__name__ == 'int':
            request.session['genre'] = genre_id
        else:
            request.session.pop('genre', None)
    if console_id:
        if type(console_id).__name__ == 'int':
            request.session['console'] = console_id
        else:
            request.session.pop('console', None)
    if sort:
        if type(sort).__name__ == 'int':
            request.session['sort'] = sort

    # Checks to see if the given key is not in the session, sets the default value for the context if true
    if 'genre' not in request.session:
        context['genre_label'] = 'All'
    if 'console' not in request.session:
        context['console_label'] = 'All'
    if 'sort' not in request.session:
        context['sort_label'] = SORT_LABELS[1]

    # A series of checks to determine which ids are set to determine how to sort and filter each queryset

    # Genre, Console and Sort
    if 'genre' in request.session and 'console' in request.session and 'sort' in request.session:
        context['games'] = Game.objects.filter(genres=request.session['genre'],
                                               console_id=request.session['console']).order_by(
            SORT_DICT[request.session['sort']])
        context['genre_label'] = Genre.objects.filter(id=request.session['genre']).first().name
        context['console_label'] = Console.objects.filter(id=request.session['console']).first().name
        context['sort_label'] = SORT_LABELS[request.session['sort']]
        return render(request, 'games/index.html', context)
    # Genre and Sort
    elif 'genre' in request.session and 'sort' in request.session:
        context['games'] = Game.objects.filter(genres=request.session['genre']).order_by(
            SORT_DICT[request.session['sort']])
        context['genre_label'] = Genre.objects.filter(id=request.session['genre']).first().name
        context['sort_label'] = SORT_LABELS[request.session['sort']]
        return render(request, 'games/index.html', context)
    # Console and Sort
    elif 'console' in request.session and 'sort' in request.session:
        context['games'] = Game.objects.filter(console_id=request.session['console']).order_by(
            SORT_DICT[request.session['sort']])
        context['console_label'] = Console.objects.filter(id=request.session['console']).first().name
        context['sort_label'] = SORT_LABELS[request.session['sort']]
        return render(request, 'games/index.html', context)
    # Genre and Console
    elif 'genre' in request.session and 'console' in request.session:
        context['games'] = Game.objects.filter(genres=request.session['genre'],
                                               console_id=request.session['console']).order_by('name')
        context['genre_label'] = Genre.objects.filter(id=request.session['genre']).first().name
        context['console_label'] = Console.objects.filter(id=request.session['console']).first().name
        return render(request, 'games/index.html', context)
    # Genre
    elif 'genre' in request.session:
        context['games'] = Game.objects.filter(genres=request.session['genre']).order_by('name')
        context['genre_label'] = Genre.objects.filter(id=request.session['genre']).first().name
        return render(request, 'games/index.html', context)
    # Console
    elif 'console' in request.session:
        context['games'] = Game.objects.filter(console_id=request.session['console']).order_by('name')
        context['console_label'] = Console.objects.filter(id=request.session['console']).first().name
        return render(request, 'games/index.html', context)
    # Sort
    elif 'sort' in request.session:
        context['games'] = Game.objects.all().order_by(SORT_DICT[request.session['sort']])
        context['sort_label'] = SORT_LABELS[request.session['sort']]
        return render(request, 'games/index.html', context)
    # Default
    else:
        context['games'] = Game.objects.all().order_by('name')
        return render(request, 'games/index.html', context)


def get_game_by_id(request, id):
    """
    Returns the details page for a game based on id.
    :param request: WSGIRequest
    :param id: int
    :return Render:HttpResponse
    """
    context = {}

    add_recently_viewed(request, id)

    # Gets all the reviews for the title
    reviews = Review.objects.filter(gameID_id=id)
    if reviews:
        context['reviews'] = reviews

    context['product'] = get_object_or_404(Game, pk=id)
    context['product_id'] = id
    return render(request, 'product_details.html', context)


def get_game_offers(request):
    """
    Returns a query set of all games where on_sale is True
    :param request: WSGIRequest
    :return: Queryset
    """
    return Game.objects.filter(on_sale=True)


@login_required
def add_review(request):
    """
    Adds a review for the product. Calculates the recommendation score based on all reviews for the title
    and sets it.
    :param request: WSGIRequest
    :return: HttpResponse
    """
    profile = Profile.objects.filter(user=request.user).first()

    if request.method == "POST":
        feedback = request.POST['feedback']
        recommend = request.POST['recommend']
        date = datetime.date.today()
        profile_id = profile.id
        product_id = request.POST['product_id']
        review = Review.objects.filter(profileID_id=profile_id, gameID_id=product_id)
        if review:
            review.update(feedback=feedback, recommend=recommend)
        else:
            Review.objects.create(recommend=recommend,
                                  feedback=feedback,
                                  datetime=date,
                                  profileID_id=profile_id,
                                  gameID_id=product_id)
        reviews = Review.objects.filter(gameID_id=request.POST['product_id'])
        recommendations = 0
        if reviews:
            for review in reviews:
                if review.recommend:
                    recommendations += 1

            recommendations /= len(reviews)
            recommendations *= 100
        game = Game.objects.filter(id=product_id).first()
        game.rating = recommendations
        game.save()

    return HttpResponse("Check")
