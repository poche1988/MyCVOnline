var NewsServices = function () {
    var removeNews = function (id, deletesuccess, errorMessage) {
        $.ajax({
            url: "/api/News/Delete/" + id,
            method: "POST",
            contentType: "aplication/Json; charset = UTF-8",
            DataType: "Json",
            success: (deletesuccess),
            error: (errorMessage)
        })
    }

    return {
        removeNews: removeNews,
    }
}();