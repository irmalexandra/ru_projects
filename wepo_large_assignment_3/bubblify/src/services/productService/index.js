import toastr from "toastr"

const service = "http://localhost:3500/api"

const getProducts = async () => {
    let products = await (fetch(service+'/bubbles').then(res => res.json()).then(data =>{return data}));
    return products ? products : [];
}

const getBundles = async () =>{
    let allBundles = await(fetch(service+'/bundles').then(res => res.json()).then(data =>{return data}));
    for (const index in allBundles){
        allBundles[index].items = await getBundleItems(allBundles[index].items)
    }
    return allBundles;
}

const getProductsByNumber = async (telephone) => {
    let productList = []
    let orderList = await (fetch(service+'/orders/'+telephone).then(res => res.json()).then(data =>{return data}));
    const products = orderList[orderList.length-1].order.products;
    for (const index in products){
        productList.push(await getProductById(products[index]));
    }
    return productList;
}

const postOrder = async ({cart, info}) =>{
    let productIdList = []
    for (const index in cart){
        productIdList.push(cart[index].id)
    }
    const requestOptions = {
        method: 'POST',
        headers: {'Content-Type': 'application/json'},
        body: JSON.stringify({order: {products:productIdList, info:info}})
    }
    const response = await (fetch(service+'/orders/'+info.telephone, requestOptions));
    if (response.ok){
        clearCart();
        toastr.success("Order successfully completed!")
        return true;
    }
    toastr.error("Something went wrong with the order, please try again.")
    return false;
}

const getBundleItems = async (itemsList) =>{
    let products = await getProducts()
    let bundle = {
        products: [],
        totalPrice: 0
    }
    for(const index in itemsList){
        let product = products.find(p => p.id === parseInt(itemsList[index]))
        bundle.products.push(product);
        bundle.totalPrice += product.price;
    }
    return bundle;
}

const getProductById = async id => {
    return await(fetch(service+'/bubbles/' + id).then(res => res.json()).then(data =>{return data}));
}

const getBundleById = async id => {
    let bundle;
    bundle = await (fetch(service + '/bundles/' + id).then(res => res.json()).then(data => {
        return data
    }));
    return bundle
}

const addToCart = id => {
    let cart = [];
    if((localStorage.getItem("cart"))){
        cart = JSON.parse(localStorage.getItem("cart"))
        if(cart.includes(id)){
            return
        }
    }
    cart.push(id)
    localStorage.setItem("cart", JSON.stringify(cart))

    toastr["success"]("<br /><br /><a href = \"/checkout\" class=\"btn clear btn-secondary\"}'>Proceed to checkout</a>", "Added to cart!")

    toastr.options = {
        "closeButton": false,
        "debug": false,
        "newestOnTop": true,
        "progressBar": true,
        "positionClass": "toast-top-right",
        "preventDuplicates": true,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": 3000,
        "extendedTimeOut": 0,
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut",
        "tapToDismiss": false
    }
}

const addBundleToCart = async bundleId => {
    let bundleItemsIds = (await getBundleById(bundleId)).items
    for (const index in bundleItemsIds){
        addToCart(bundleItemsIds[index])
    }
}

const getCartItems = async () => {
    let return_list = []
    let cart = localStorage.getItem("cart")
    if(cart){
        cart = JSON.parse(cart)
    }
    else{
        return return_list
    }
    for(const index in cart){
        return_list.push(await getProductById(cart[index]))
    }
    return return_list
}

const clearCart = () =>
    localStorage.setItem("cart", "");


export {
    getProducts,
    getProductById,
    getBundles,
    addToCart,
    addBundleToCart,
    getCartItems,
    clearCart,
    postOrder,
    getProductsByNumber
};