$(document).ready(function () {
    $('#review-button').on('click', function (e) {
        var recommend = document.getElementById('id_recommend').value

        console.log(recommend)
        var feedback = $.trim($('#id_feedback').val());
        var datetime = $.trim($('#id_datetime').val());
        var product_id = document.getElementById('product_id').innerHTML

        let csrf_token = document.getElementsByName('csrfmiddlewaretoken')[0].value
        $.ajax({
            headers: { "X-CSRFToken": csrf_token },
            url : "/games/add_review",
            type: "POST",
            data : {
                feedback: feedback,
                recommend : recommend,
                datetime: datetime,
                product_id: product_id
            }
        }).done(function(data) {
            window.location = window.location;

        });
     })

})


$('#id_feedback').keydown(function() {

  var characterCount = $(this).val().length,
      current = $('#current'),
      maximum = $('#maximum'),
      theCount = $('#the-count');

  current.text(characterCount);


  /*This isn't entirely necessary, just playin around*/
  if (characterCount < 150) {
    current.css('color', '#666');
  }
  if (characterCount > 150 && characterCount < 250) {
    current.css('color', '#6d5555');
  }
  if (characterCount > 250 && characterCount < 350) {
    current.css('color', '#793535');
  }
  if (characterCount > 350 && characterCount < 450) {
    current.css('color', '#841c1c');
  }
  if (characterCount > 450 && characterCount < 550) {
    current.css('color', '#8f0001');
  }

  if (characterCount >= 550) {
    maximum.css('color', '#8f0001');
    current.css('color', '#8f0001');
    theCount.css('font-weight','bold');
  } else {
    maximum.css('color','#666');
    theCount.css('font-weight','normal');
  }
});