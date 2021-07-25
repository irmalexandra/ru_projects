from django.shortcuts import render, get_object_or_404

# Create your views here.
from consoles.models import Console
from main.views import add_recently_viewed

SORT_DICT = {
    1: 'name',
    2: '-name',
    3: 'price',
    4: '-price',
    5: '-release_date',
    6: 'release_date',

}
CONSOLE_SORT_LABELS = {
    1: 'A-Z',
    2: 'Z-A',
    3: 'Price: Low to High',
    4: 'Price: High to Low',
    5: 'Latest',
    6: 'Oldest',
}


def console_sort_view(request, id):
    """
    Calls the sorter with the sort id
    :param request: WSGIRequest
    :param id: int
    :return sorter:HttpResponse
    """
    return sorter(request, id)


def console_default_view(request):
    """
    Calls the sorter with the default values
    and resets the session, turning off all filtering and sorting
    :param request: WSGIRequest
    :param id: int
    :return sorter:HttpResponse
    """
    request.session.pop('console_sort', None)
    return sorter(request)


def sorter(request, sort=None):
    """
    Sets the context for the Render based on optional parameters. Sorts the
    console queryset before adding it to the context
    :param request: WSGIRequest
    :param sort: int
    :return Render:HttpResponse
    """
    context = {"consoles_tab": "active",
               'console_sort_dict': CONSOLE_SORT_LABELS}

    if sort:
        if type(sort).__name__ == 'int':
            request.session['console_sort'] = sort

    if 'console_sort' not in request.session:
        context['console_sort_label'] = CONSOLE_SORT_LABELS[1]

    if 'console_sort' in request.session:
        context['consoles'] = Console.objects.all().order_by(SORT_DICT[request.session['console_sort']])
        context['console_sort_label'] = CONSOLE_SORT_LABELS[request.session['console_sort']]
        return render(request, 'consoles/index.html', context)

    else:
        context['consoles'] = Console.objects.all().order_by('name')
        return render(request, 'consoles/index.html', context)


def get_console_by_id(request, id):
    """
    Returns the details page for a console based on id.
    :param request: WSGIRequest
    :param id: int
    :return Render:HttpResponse
    """
    add_recently_viewed(request, id, False)
    context = {'product': get_object_or_404(Console, pk=id)}
    return render(request, 'product_details.html', context)


def get_console_offers(request):
    """
    Returns a query set of all games where on_sale is True
    :param request: WSGIRequest
    :return: Queryset
    """
    return Console.objects.filter(on_sale=True)
