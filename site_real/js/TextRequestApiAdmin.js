function SetText(TextName,Text){
    return fetch(`/text/${TextName}`,{
        method:"POST",
        headers:{
            'Content-Type': 'text/plain'
        },
        body: Text
    })
}