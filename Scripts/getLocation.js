let latitude, longitude = "";
if (navigator.geolocation) {
    navigator.geolocation.getCurrentPosition(onSuccess, onError);
} else {
    alert("Konum bilgisi alınamyor.")
}
function onSuccess(position) {
    let latitude = position.coords.latitude;
    let longitude = position.coords.longitude;

    const api_key = "93cd2f03f9a4473eab00b48326a0c1df"
    const url = `https://api.opencagedata.com/geocode/v1/json?q=${latitude}+${longitude}&key=${api_key}`;
    

    fetch(url)
        .then(response => response.json())
        .then(result => console.log(result.results[0]));
    
}

function onError(error) {
    if (error.code == 1) {
        alert("Kullanıcı Erişim İznini Reddetti");
    } else if (error.code == 2) {
        alert("Konum Alınamadı");
    }
    else {
        alert("Bir hata oluştu");
    }
}