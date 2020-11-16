"use strict";

var connectionPubSub = new signalR.HubConnectionBuilder().withUrl("/pubSubHub").build();
var connectionQueue = new signalR.HubConnectionBuilder().withUrl("/queueHub").build();

connectionPubSub.on("ReceivePubSubMessage", function (message) {
    writeToList("pub_sub_messages", message);
});

connectionQueue.on("ReceiveQueueMessage", function (message) {
    writeToList("queue_messages", message);
});

function writeToList(listId, message) {
    var msg = message
        .replace(/&/g, "&amp;")
        .replace(/</g, "&lt;")
        .replace(/>/g, "&gt;");

    var li = document.createElement("li");
    li.textContent = msg;

    var list = document.getElementById(listId);

    if (list.getElementsByTagName("li").length >= 9) {
        list.removeChild(list.childNodes[0]);
    }

    list.appendChild(li);
};

connectionPubSub.start().catch(function (err) {
    return console.error(err.toString());
});

connectionQueue.start().catch(function (err) {
    return console.error(err.toString());
});