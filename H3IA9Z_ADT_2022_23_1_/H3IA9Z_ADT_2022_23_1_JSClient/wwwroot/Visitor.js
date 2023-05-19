let visitors = [];
let connection = null;
getdata();
setupSignalR();
let visitorIdToUpdate = -1;

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:18972/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("VisitorCreated", (user, message) => {
        getdata();
    });

    connection.on("VisitorDeleted", (user, message) => {
        getdata();
    });
    connection.on("VisitorUpdated", (user, message) => {
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
    await fetch('http://localhost:18972/visitor')
        .then(x => x.json())
        .then(y => {
            visitors = y;
            display();
        });
}
function display() {
    document.getElementById('resultarea').innerHTML = "";
    visitors.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.id + "</td><td>"
            + t.name + "</td><td>" + t.city + "</td><td>" + t.email + "</td><td>" +
            `<button type="button" onclick="remove(${t.id})">Delete</button>` +
            `<button type="button" onclick="showupdate(${t.id})">Update City</button>`
            + "</td></tr>";
    });
    document.getElementById('visitorname').value = "";
    document.getElementById('visitorcity').value = "";
    document.getElementById('visitoremail').value = "";
}
function showupdate(id) {
    document.getElementById('visitorcityToUpdate').value = visitors.find(t => t['id'] == id)['city'];
    document.getElementById('updateformdiv').style.display = 'flex';
    visitorIdToUpdate = id;
}
function remove(id) {
    fetch('http://localhost:18972/visitor/' + id, {
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
    let Visitorname = document.getElementById('visitorname').value;
    let Visitorcity = document.getElementById('visitorcity').value;
    let Visitoremail = document.getElementById('visitoremail').value;

    fetch('http://localhost:18972/visitor', {
        method: 'POST',
        headers: {
            'Accept': 'application/json, text/plain, */*',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(
            { City: Visitorcity, Name: Visitorname, Email: Visitoremail })
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
    let VisitorcityToUpd = document.getElementById('visitorcityToUpdate').value;
    let Visitoremail = visitors.find(t => t['id'] == visitorIdToUpdate)['email'];
    let Visitorname = visitors.find(t => t['id'] == visitorIdToUpdate)['name'];
    fetch('http://localhost:18972/visitor', {
        method: 'PUT',
        headers: {
            'Accept': 'application/json, text/plain, */*',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(
            { Name: Visitorname, Email: Visitoremail, City: VisitorcityToUpd, Id: visitorIdToUpdate })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}