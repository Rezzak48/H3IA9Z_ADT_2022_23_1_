let reservations = [];
let connection = null;
getdata();
setupSignalR();
let reservationIdToUpdate = -1;

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:18972/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("ReservationCreated", (user, message) => {
        getdata();
    });

    connection.on("ReservationDeleted", (user, message) => {
        getdata();
    });
    connection.on("ReservationUpdated", (user, message) => {
        getdata();
    });

    connection.onclose(async () => {
        await start();
    });
    start();
}
async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};
async function getdata() {
    await fetch('http://localhost:18972/Reservations')
        .then(x => x.json())
        .then(y => {
            reservations = y;
            display();
        });
}
function display() {
    document.getElementById('resultarea').innerHTML = "";
    reservations.forEach(t => {
        // if (t.visitorId != null && t.movieId != null) {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.id + "</td><td>"
            + t.visitorId + "</td><td>" + t.movieId + "</td><td>" + t.dateTime + "</td><td>" +
            `<button type="button" onclick="remove(${t.id})">Delete</button>` +
            `<button type="button" onclick="showupdate(${t.id})">Update Date</button>`
            + "</td></tr>";
        //  }
    });
    document.getElementById('reservationvisid').value = "";
    document.getElementById('reservationmovieid').value = "";
    document.getElementById('reservationdate').value = "";
}
function showupdate(id) {
    document.getElementById('reservationdateToUpdate').value = reservations.find(t => t['id'] == id)['dateTime'];
    document.getElementById('updateformdiv').style.display = 'flex';
    reservationIdToUpdate = id;
}
function remove(id) {
    fetch('http://localhost:18972/reservations/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}
function create() {
    let Reservisitorid = document.getElementById('reservationvisid').value;
    let Resermovieid = document.getElementById('reservationmovieid').value;
    let Reserdate = document.getElementById('reservationdate').value;

    fetch('http://localhost:18972/reservations', {
        method: 'POST',
        headers: {
            'Accept': 'application/json, text/plain, */*',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(
            { VisitorId: Reservisitorid, MovieId: Resermovieid, DateTime: Reserdate })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}
function update() {
    document.getElementById('updateformdiv').style.display = 'none';
    let ReserdateToUpd = document.getElementById('reservationdateToUpdate').value;
    let Reservisitorid = reservations.find(t => t['id'] == reservationIdToUpdate)['visitorid'];
    let Resermovieid = reservations.find(t => t['id'] == reservationIdToUpdate)['movieid'];
    fetch('http://localhost:18972/Reservations', {
        method: 'PUT',
        headers: {
            'Accept': 'application/json, text/plain, */*',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(
            { VisitorId: Reservisitorid, MovieId: Resermovieid, DateTime: ReserdateToUpd, Id: reservationIdToUpdate })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}