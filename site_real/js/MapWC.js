/**
 * @typedef {Object} LatLng
 * @property {Number} lat
 * @property {Number} lng
*/

//import { default as Maps } from "https://js.api.here.com/v3/3.1/mapsjs.bundle.js";
const Maps=H;
let apiKey = "PMFoDa_dg5LnVM4X9dVHmZuzLg_haag5crYUoYoregg";
class MapElement extends HTMLElement {
    constructor() {
        super();
        this.isDragging=true;
        this.attachShadow({ mode: 'open' });
        this.div = document.createElement("div");
        this.div.style.width = "100%";
        this.div.style.height = "460px";
        this.shadowRoot.appendChild(this.div);
        this.platform = new Maps.service.Platform({
            'apikey': apiKey
        });
        this.defaultLayers = this.platform.createDefaultLayers();
        console.log(Maps);
    }
    connectedCallback() {
        this.map = new Maps.Map(
            this.div,
            this.defaultLayers.vector.normal.map,
            {
                zoom: 8,
                center: { lat: 52.5, lng: 13.4 }
            });
            this.mapEvents=new Maps.mapevents.MapEvents(this.map);
        this.map.addEventListener('pointerdown',(e)=>{
            this.isDragging=false;
        });
        this.map.addEventListener('drag',(e)=>{
            this.isDragging=true;
        });
        this.map.addEventListener('click',(e)=>{
            console.log(e);
            if(!this.isDragging){
                console.log(e);
            }
        });
        console.log(this.map.addEventListener);
        this.behavior = new Maps.mapevents.Behavior(this.mapEvents);
        
        //console.log(this.ui.getMap());
    }
    /**
     * 
     * @param {Country} country
     */
    showCountry(country) {

    }
    /**
     * 
     * @param {Country} country
     * @returns {String} 
     */
    countryToText(country) {
        return (country.exists ? JSON.stringify(country) : `${country.code} has no data in the database`);
    }

}
window.customElements.define("g-map", MapElement);