from django.forms import ModelForm
from django import forms

from users.models import Review


class ReviewForm(ModelForm):
    """ The Review form """
    CHOICES = [(True, 'Yes'),
               (False, 'No')]

    recommend = forms.CharField(label='Would you recommend this game?', widget=forms.Select(choices=CHOICES))
    feedback = forms.CharField(label='Comments', required=False, max_length=999, widget=forms.Textarea(attrs={'rows': 4, 'cols': 40}))

    class Meta:
        model = Review
        fields = ('recommend', 'feedback')
