from users.forms.login_form import LoginForm
from users.forms.register_form import RegisterForm
from users.forms.review_form import ReviewForm
from users.models import Review


def login_form(request):
    """
    Returns an login form with request
    @param request:
    @return context:
    """
    return {
        "login_form": LoginForm(request)
    }


def register_form(request):
    """
    Returns an empty register form
    @param request:
    @return context:
    """
    return {
        'register_form': RegisterForm(auto_id='False')
    }


def review_form(request):
    """
    Returns either an empty review form or a review form tied to current profileID and current gameID
    @param request:
    @return:
    """
    current_user = request.user
    if current_user.is_authenticated:
        product_id = request.path.split("/")[-1]
        if product_id.isdigit():
            profile_id = request.user.profile.id
            review = Review.objects.filter(profileID_id=profile_id, gameID_id=product_id).first()
            if review:
                return {
                    'review_form': ReviewForm(instance=review)
                }
    return {
        'review_form': ReviewForm()
    }
