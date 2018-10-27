var NewsController = function (newsServices) {
    var $btn;
    var id;

    var init = function (container) {
        $(container).on("click", ".js-delete-news", deleteNews)
    }

    var deleteNews = function () {
        $btn = $(this);
        id = $btn.data('id')
        var dialog = bootbox.dialog({
            message: 'Are you sure you want to delete this News?',
            title: "Confirm",
            buttons:
                {
                    no: {
                        label: "No",
                        className: "btn-default",
                        callback: function ()
                        { bootbox.hideAll() }
                    },
                    yes: {
                        label: "Yes",
                        className: "btn-danger",
                        callback: function (result) {
                            newsServices.removeNews(id, deletesuccess, errorMessage)
                        }
                    }
                }
        });
    }

    var deletesuccess = function () {
        $btn.closest("li").fadeOut(800, function () {
            $btn.closest("li").remove();
        });
    }

    var errorMessage = function () {
        alert("something failed!")
    }

    return {
        init: init
    }
}(NewsServices);