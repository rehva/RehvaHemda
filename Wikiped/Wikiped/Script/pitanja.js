$(document).ready(function () {
   
    $(".vote .vote-up-off").click(function () {
        var id = $(this).parent().children('input').val();
    });

    $(".vote .vote-down-off").click(function () {
        var id = $(this).parent().children('input').val();
    });

});