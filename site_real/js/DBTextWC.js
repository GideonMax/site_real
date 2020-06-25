import GetText from './TextRequestApi.js';
console.log(GetText);
class DBTextdiv extends HTMLElement{
    static get observedAttributes(){
        return ['TextName'];
    }
    constructor(){
        super();
        this.div=document.createElement('div');
    }
    connectedCallback(){
        this.appendChild(this.div);
        GetText(this.getAttribute('TextName')).then(val => { this.div.innerHTML = val });
    }
    attributeChangedCallback(name, oldvalue, newvalue) {
        GetText(newValue).then(val=>{this.div.innerHTML=val});
    }
}

window.customElements.define("db-div",DBTextdiv)