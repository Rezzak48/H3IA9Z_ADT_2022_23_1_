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
            + t.name + "</td><td>" + t.category + "</td><td>" + t.price + "$</td><td>" +
            `<button type="button" onclick="remove(${t.id})">Delete</button>` +
            `<button type="button" onclick="showupdate(${t.id})">Update Cost</button>`
            + "</td></tr>";
    });
    document.getElementById('moviename').value = "";
    document.getElementById('moviecategory').value = "";
    document.getElementById('moviecost').value = "";
}
function showupdate(id) {
    document.getElementById('moviecostToUpdate').value = movies.find(t => t['id'] == id)['price'];
    document.getElementById('updateformdiv').style.display = 'flex';
    movieIdToUpdate = id;
}
function remove(id) {
    fetch('http://localhost:18972/movie/' + id, {
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
    let Movietcost = document.getElementById('movietcost').value;
    fetch('http://localhost:18972/movie', {
        method: 'POST',
        headers: {
            'Accept': 'application/json, text/plain, */*',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(
            { Category: Moviecategory, Name: Moviename, Price: Movietcost })
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
    //let MovienameToUpd = document.getElementById('movienameToUpdate').value;
    //let MoviecategoryToUpd = document.getElementById('moviecategoryToUpdate').value;
    let MovietcostToUpd = document.getElementById('moviecostToUpdate').value;
    let Movietcategory = movies.find(t => t['id'] == movieIdToUpdate)['category'];
    let Movietname = movies.find(t => t['id'] == movieIdToUpdate)['name'];
    fetch('http://localhost:18972/movie', {
        method: 'PUT',
        headers: {
            'Accept': 'application/json, text/plain, */*',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(
            //{ Category: MoviecategoryToUpd, Name: MovienameToUpd, Id: movieIdToUpdate })
            { Name: Movietname, Category: Movietcategory, Price: MovietcostToUpd, Id: movieIdToUpdate })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}