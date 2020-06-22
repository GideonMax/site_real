/**
 * @typedef {Object} LatLng
 * @property {Number} lat
 * @property {Number} lng
*/
//import "./Maps/Charts/node_modules/core-js/index.js";
//import { default as Maps } from "https://js.api.here.com/v3/3.1/mapsjs.bundle.js";
import * as MapsCore from './Maps/Charts/core.js';
import * as Maps from './Maps/Charts/maps.js';
import theme from './Maps/Charts/themes/animated.js';
import { default as GeoData } from './Maps/geodata/worldHigh.js';
import GeoCode from './geocoding.js';
import {getCountryData}from './countryData.js';
//const Maps=H;
//let apiKey = "PMFoDa_dg5LnVM4X9dVHmZuzLg_haag5crYUoYoregg";
console.log(Maps);
console.log(MapsCore.create);
class MapElement extends HTMLElement {
    constructor() {
        super();
        this.attachShadow({ mode: 'open' });
        this.div = document.createElement("div");
        this.div.style.width = "100%";
        this.div.style.height = "460px";
        this.shadowRoot.appendChild(this.div);
        this.SetUpMap();
        /*
        this.isDragging=true;
        this.platform = new Maps.service.Platform({
            'apikey': apiKey
        });
        this.defaultLayers = this.platform.createDefaultLayers();
        console.log(Maps);*/
    }
    SetUpMap() {
        MapsCore.useTheme(theme);
        this.map = MapsCore.create(this.div, Maps.MapChart);
        this.map.geodata = GeoData;
        this.map.projection = new Maps.projections.Miller();
        this.map.panBehavior="rotateLong";
        this.series = this.map.series.push(new Maps.MapPolygonSeries());
        this.series.useGeodata = true;
        this.polygonTemplate = this.series.mapPolygons.template;
        this.polygonTemplate.events.on("hit", (event)=>{
            this.map.zoomToMapObject(event.target);
            let coords=this.map.svgPointToGeo(event.svgPoint);
            GeoCode(coords.latitude,coords.longitude).then(getCountryData).then(this.showCountry);
        });
        this.series.exclude=["AQ"];
    }
    connectedCallback() {
        /*
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
        */
        //console.log(this.ui.getMap());
    }
    /**
     * 
     * @param {Country} country
     */
    showCountry(country) {
        console.log(country);
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