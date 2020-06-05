const uri = 'api/Orders';
let orders = [];

function addorder(id,userid) {

    const order = {
        TattooId: id,
        UserId: userid,
    };

    console.log(JSON.stringify(order));
    fetch(uri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(order)
    })
        .then(response => response.json())
        .catch(error => console.error('Unable to add order.', error));
}

function deleteOrder(id) {
    uril = "https://localhost:44386/api/Orders"
    fetch(`${uril}/${id}`, {
        method: 'DELETE'
    })
        .catch(error => console.error('Unable to delete color.', error));

    setTimeout(
        function () {

            location.reload();

        }, 500);

}