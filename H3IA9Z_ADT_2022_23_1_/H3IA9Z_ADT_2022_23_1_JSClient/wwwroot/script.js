let movies = [];
let connection = null;
getdata();
setupSignalR();
let movieIdToUpdate = -1;

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:18972/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("MovieCreated", (user, message) => {
        getdata();
    });

    connection.on("MovieDeleted", (user, message) => {
        getdata();
    });
    connection.on("MovieUpdated", (user, message) => {
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
    await fetch('http://localhost:18972/movie')

        .then(x => x.json())
        .then(y => {
            movies = y;
            console.log(movies);
            display();
        });
}
function display() {
    document.getElementById('resultarea').innerHTML = "";
    movies.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.id + "</td><td>"
            + t.name + "</td><td>" +
            `<button type="button" onclick="remove(${t.id})">Delete</button>` +
            `<button type="button" onclick="showupdate(${t.id})">Update</button>`
            + "</td></tr>";
    });
}
function showupdate(id) {
    document.getElementById('movienameToUpdate').value = movies.find(t => t['id'] == id)['name'];
    document.getElementById('moviecategoryToUpdate').value = movies.find(t => t['id'] == id)['category'];
    document.getElementById('updateformdiv').style.display = 'flex';
    movieIdToUpdate = id;
}
function remove(id) {
    fetch('http://localhost:18972/movie' + id, {
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
    let Moviename = document.getElementById('moviename').value;
    let Moviecategory = document.getElementById('moviecategory').value;
    fetch('http://localhost:18972/movie', {
        method: 'POST',
        headers: {
            'Accept': 'application/json, text/plain, */*',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(
            { Category: Moviecategory, Name: Moviename })
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
    let MovienameToUpd = document.getElementById('movienameToUpdate').value;
    let MoviecategoryToUpd = document.getElementById('moviecategoryToUpdate').value;
    fetch('http://localhost:18972/movie', {
        method: 'PUT',
        headers: {
            'Accept': 'application/json, text/plain, */*',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(
            { Category: MoviecategoryToUpd, Name: MovienameToUpd, Id: movieIdToUpdate })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}