/**
 * @typedef {Object} Country
 * @property {Number} ID
 * @property {String} code
 * @property {String} CountryName 
 * @property {String} OfficialArticle 
 * @property {String} UserArticle 
 * @property {Boolean} exists
 */
//#region MapsImports
import * as MapsCore from './Maps/Charts/core.js';
import * as Maps from './Maps/Charts/maps.js';
import theme from './Maps/Charts/themes/animated.js';
import { default as GeoData } from './Maps/geodata/worldHigh.js';
//#endregion
//#region MyOwnImports
import GeoCode from './geocoding.js';
import {getCountryData} from './countryData.js';
import * as CommentApi from './ForumApi.js';
//#endregion
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
    /**
     * 
     * @param {Country} country
     */
    showCountry(country) {
        let div = document.createElement("div");
        div.className="popup";
        let exitButton = document.createElement("button");
        exitButton.onclick=()=>{
            div.remove();
        };
        div.appendChild(exitButton);
        let editButton = document.createElement("button");
        editButton.onclick=this.PopUpEditWindow(country);
        div.appendChild(editButton);
        if(!country.exists){
            let text=document.createElement("div");
            text.innerHTML="<h1>על המדינה הזאת אין לנו מידע</h1><br>אולי תוסיף?";
        }
        else{
            div.append(`<h1>${country.CountryName}</h1>`);
            if(country.OfficialArticle&&country.OfficialArticle!=null){
                div.append("<h2>ערך רשמי</h2>");
                div.append(country.OfficialArticle);
            }
            if(country.UserArticle&&country.UserArticle!=null){
                div.append("<h2>ערך של משתמשי האתר</h2>");
                div.append(country.UserArticle);
            }
        }
        CommentApi.GetCountryComments(country.ID).then(comments=>{
            if(comments.length==0){
                div.append("<h3>אין תגובות</h3>");
                return;
            }
            let list=document.createElement("ol");
            for(var comment of comments){
                let item=document.createElement("li");
                item.innerHTML=`${comment.UserName}:<br>${comment.Body}<br>`;
                list.appendChild(item);
            }
            div.appendChild(list);
        })
    }
    /**
     * 
     * @param {Country} country 
     */
    PopUpEditWindow(country){

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