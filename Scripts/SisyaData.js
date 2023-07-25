
function getValue(url, data, callback) {
    var getUrl = window.location;
    var baseUrl = getUrl.protocol + "//" + getUrl.host + "/";
    //console.log(baseUrl);
    $(document).trigger('post_oncesi', baseUrl + url, data);
    axios.post(baseUrl + url, data)
        .then(function (res) {
            $(document).trigger('post_sonrasi', res);
            if (typeof callback === 'function') callback(res.data, null);
        })
        .catch(function (err) {
            alert("HATA OLUŞTU!");
            if (typeof callback === 'function') callback(false, err);
        });
}
