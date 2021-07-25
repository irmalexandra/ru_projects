




(function (globalbObj) {

    function MakeBelieveElement(nodes){
        this.nodes = nodes;
    }

    // MakeBelieveElement.prototype.getLength = function(){
    //     return this.nodes.length;
    // }

    // #4
    MakeBelieveElement.prototype.getParent = function(filter = null){
        let parentList = []
        let filteredItems = [];


        for(let i = 0; i < this.nodes.length; i++) {
            if (!parentList.includes(this.nodes[i].parentNode)) {
                parentList.push(this.nodes[i].parentNode);
            }
        }
        if(filter){
            filteredItems = document.querySelectorAll(filter);
            let filteredParentList = []
            if(filteredItems.length > 0){
                for(let i = 0; i < filteredItems.length; i++){
                    if(parentList.includes(filteredItems[i])){
                        filteredParentList.push(filteredItems[i])
                    }
                }
            }
            parentList = filteredParentList

        }
        this.nodes = parentList;
        return this;
    }

    // #5
    MakeBelieveElement.prototype.getGrandParent = function(filter = null){
        let grandParentList = []
        let filteredItems = [];


        for(let i = 0; i < this.nodes.length; i++) {
            if (!grandParentList.includes(this.nodes[i].parentNode.parentNode)) {
                grandParentList.push(this.nodes[i].parentNode.parentNode);
            }
        }
        if(filter){
            filteredItems = document.querySelectorAll(filter);
            let filteredGrandParentList = []
            if(filteredItems.length > 0){
                for(let i = 0; i < filteredItems.length; i++){
                    if(grandParentList.includes(filteredItems[i])){
                        filteredGrandParentList.push(filteredItems[i])
                    }
                }
            }
            grandParentList = filteredGrandParentList

        }
        this.nodes = grandParentList;
        return this;
    }

    // #6
    MakeBelieveElement.prototype.getAncestor = function(filter = null){
        let ancestorList = []
        let filteredItems = [];


        for(let i = 0; i < this.nodes.length; i++) {
            if (!ancestorList.includes(this.nodes[i].parentNode.parentNode.parentNode)) {
                ancestorList.push(this.nodes[i].parentNode.parentNode.parentNode);
            }
        }
        if(filter){
            filteredItems = document.querySelectorAll(filter);
            let filteredAncestorList = []
            if(filteredItems.length > 0){
                for(let i = 0; i < filteredItems.length; i++){
                    if(ancestorList.includes(filteredItems[i])){
                        filteredAncestorList.push(filteredItems[i])
                    }
                }
            }
            ancestorList = filteredAncestorList

        }
        this.nodes = ancestorList;
        return this;
    }

    // #7
    MakeBelieveElement.prototype.onClick = function(funct){
        for(let i = 0; i < this.nodes.length; i++){
            this.nodes[i].addEventListener("click", funct);
        }
    }

    MakeBelieveElement.prototype.getChildren = function(){
        this.nodes = this.nodes[0].children;
        return this
    }

    // #8
    MakeBelieveElement.prototype.insertText = function(text){
        for(let i = 0; i < this.nodes.length; i++){
            this.nodes[i].innerHTML = text;
        }
    }

    // #9
    MakeBelieveElement.prototype.append = function(appendee){
        if(appendee instanceof HTMLParagraphElement){
            console.log(appendee.outerHTML)
            for(let i = 0; i < this.nodes.length; i++){
                this.nodes[i].append(appendee);
            }
        }
    else{
            for(let i = 0; i < this.nodes.length; i++){

                this.nodes[i].innerHTML += appendee
            }
        }
    }

    // #10
    MakeBelieveElement.prototype.prepend = function(prependee){
        console.log(prependee + " <--- should be no?")
        if(typeof prependee == "object"){
            for(let i = 0; i < this.nodes.length; i++){
                this.nodes[i].prepend(prependee.cloneNode());
            }
        }
        else{
            for(let i = 0; i < this.nodes.length; i++){
                this.nodes[i].innerHTML = prependee + this.nodes[i].innerHTML
            }
        }
    }

    // #11
    MakeBelieveElement.prototype.delete = function(){
        for(let i = 0; i < this.nodes.length ; i++){
            this.nodes[i].remove();
        }
    }

    // #12
    MakeBelieveElement.prototype.ajax = function (reqInfo){
        const request = new XMLHttpRequest();

        let method = reqInfo.method || "GET";
        let timeout = reqInfo.timeout || 0;
        let data = reqInfo.data || {};
        let headers = reqInfo.headers || {};
        let successFunc = reqInfo.success || null;
        let failFunc = reqInfo.fail || null;
        let beforeSendFunc = reqInfo.beforeSend || null;

        request.onreadystatechange = function(){
            if (request.readyState === 1){
                if (beforeSendFunc) {
                   beforeSendFunc(this);
                }
            }
            if (request.readyState === 4 && request.statusText === "OK"){
                if (successFunc){
                    successFunc(this.response);
                }
            }
            if (request.readyState === 4 && request.statusText === "Not Found"){
                if (failFunc){
                    failFunc(this.error)
                }
            }
        };

        request.open(method, reqInfo.url, true);
        // Set Headers
        let keyNames = Object.keys(headers);
        for(let i = 0; i < keyNames.length ; i++){
            request.setRequestHeader(keyNames[i], headers[keyNames[i]]);
        }

        // Set Timeout
        request.timeout = timeout;

        // Send Request
        request.send((Object.keys(data).length === 0 || method !== "POST") ? null : data)
    }

    // #13
    MakeBelieveElement.prototype.css = function (element, value){
        for(let i = 0; i < this.nodes.length; i++){
            this.nodes[i].style[element] = value;
        }
    }

    // #14
    MakeBelieveElement.prototype.toggleClass = function (someClass) {
        for(let i = 0; i < this.nodes.length; i++) {
            this.nodes[i].classList.toggle(someClass);
        }
    }

    // #15
    MakeBelieveElement.prototype.onSubmit = function (submitFunc){
        for(let i = 0; i < this.nodes.length; i++){
            this.nodes[i].addEventListener("submit", submitFunc)
        }
    }

    // #16
    MakeBelieveElement.prototype.onInput = function (inputFunc) {
        for(let i = 0; i < this.nodes.length; i++){
            this.nodes[i].addEventListener("input", inputFunc)
        }
    }

    // #2
    function query(cssSelector){
        return new MakeBelieveElement(document.querySelectorAll(cssSelector));
    }

    // #1
    globalbObj.__ = query;
    globalbObj.__.ajax = MakeBelieveElement.prototype.ajax; // So that __.ajax can be called without parenthesis
})(window);

// let reqInfo = {
//     url: 'https://jsonplaceholder.typicode.com/todos/1',
//     method: 'GET',
//     timeout: 1000,
//     data: {},
//     headers: {
//         'Authorization': 'my-secret-key',
//     },
//     success: function (resp){
//         console.log("This is success: ", resp)
//         return resp
//     },
//     fail: function (error) {
//         console.log("This is fail: ", error)
//     },
//     beforeSend: function (xhr){
//         console.log("This is before: ", xhr)
//     }
//
// }
// __.ajax(reqInfo)


// __('#theForm').onSubmit(function (evt){
//     console.log(evt);
//     console.log("something plz")
//     evt.preventDefault();
// })
// __("#lokiid").css('margin-bottom', '10px')
// console.log(__(".footerDiv").getParent().getLength())
// __("#inputField").onClick(function (event){
//     console.log(event.target.value)
// })
let thing = document.createElement('p')
let thing2 = document.createTextNode("yesman")
thing.appendChild(thing2)
console.log(thing)
__(".child").append(thing)
__(".child").prepend(document.createElement('p').appendChild(document.createTextNode("No")))
// __("#mainContent :nth-child(2)").delete()
//__(".child").append("<p>kasfdakodfhsadfgksdlfkjsdgkjsdg</p>")


















