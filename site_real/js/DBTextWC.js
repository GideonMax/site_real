class DBTextdiv extends HTMLElement{
    static get observedAttributes(){
        return ['TextName'];
    }
    constructor(){
        super();
        this.div=document.createElement('div');
        this.appendChild(this.div);
    }
    connectedCallback(){
        var name = this.getAttribute('TextName');
        GetText(name).then(val=>{this.div.innerHTML=val});
    }
}

window.customElements.define("db-div",DBTextdiv)