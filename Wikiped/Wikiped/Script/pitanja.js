$(document).ready(function () {

    function userVoteUp(tempVal) {
        var obj = $(tempVal);
        var id = $(tempVal).parent().children('input').val();
        var voteNumber = $(tempVal).parent().children('.vote-count-post').text();
        var mainQu = $(tempVal).parent().hasClass('mainQuestion');

        $.ajax({
            url: '/pitanja/VoteUp',
            data: { 'id': id, 'mainQuestion': mainQu },
            type: "post",
            cache: false,
            success: function (data) {
                if (data.voteNumber == -1) {
                    // alert("vec ste glasali");
                }
                $(obj).parent().children('.vote-count-post').text(data.voteNumber);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                console.log(xhr);
            }
        });
    }
    function userVoteDown(tempVal) {
        var obj = $(tempVal);
        var id = $(tempVal).parent().children('input').val();
        var voteNumber = $(tempVal).parent().children('.vote-count-post').text();
        var mainQu = $(tempVal).parent().hasClass('mainQuestion');

        $.ajax({
            url: '/pitanja/VoteDown',
            data: { 'id': id, 'mainQuestion': mainQu },
            type: "post",
            cache: false,
            success: function (data) {
                if (data.voteNumber == -1) {
                    //alert("vec ste glasali");
                }
                $(obj).parent().children('.vote-count-post').text(data.voteNumber);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                console.log(xhr);
            }
        });
    }

    $(".vote .vote-up-off").click(function () {
        userVoteUp($(this));
    });

    $(".vote .vote-down-off").click(function () {
        userVoteDown($(this));
    });

    $(".helpful .btnYes").click(function () {
        userVoteUp($(this));
    });
    $(".helpful .btnNo").click(function () {
        userVoteDown($(this));
    });
    $(".textComment").click(function () {
        $(this).children('.comment').toggle();
    });
    $(".btnComment").button();
});