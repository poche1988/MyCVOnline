var JobsServices = function () {
    var removeJob = function (id, deletesuccess, errorMessage) {
        $.ajax({
            url: "/api/Jobs/Delete/" + id,
            method: "POST",
            contentType: "aplication/Json; charset = UTF-8",
            DataType: "Json",
            success: (deletesuccess),
            error: (errorMessage)
        })
    }

    
    return {
        removeJob: removeJob,
    }
}();