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
import { getCountryData, setCountryData } from './countryData.js';
import * as CommentApi from './ForumApi.js';
//#endregion
//const Maps=H;
//let apiKey = "PMFoDa_dg5LnVM4X9dVHmZuzLg_haag5crYUoYoregg";
class MapElement extends HTMLElement {
    constructor() {
        super();
        this.commentDiv = document.createElement("div");
        this.articleDiv = document.createElement("div");
        this.articleDiv.className = "popup";
        this.attachShadow({ mode: 'open' });
        let style = document.createElement("link");
        style.rel = "stylesheet";
        style.href = "/css/Map.css";
        this.shadowRoot.appendChild(style);
        this.mapDiv = document.createElement("div");
        this.mapDiv.style.width = "100%";
        this.mapDiv.style.height = "460px";
        this.shadowRoot.appendChild(this.mapDiv);
        this.SetUpMap();
        /*this.exitButton.addEventListener("load",()=>{
            console.log("loaded");
            this.exitButton.addEventListener("click",(event) => {
                console.log("yes");
                this.articleDiv.childNodes.forEach(val=>val.remove());
                this.articleDiv.remove();
            });
        })*/
    }
    SetUpMap() {
        MapsCore.useTheme(theme);
        this.map = MapsCore.create(this.mapDiv, Maps.MapChart);
        this.map.geodata = GeoData;
        this.map.projection = new Maps.projections.Miller();
        this.series = this.map.series.push(new Maps.MapPolygonSeries());
        this.series.useGeodata = true;
        this.polygonTemplate = this.series.mapPolygons.template;
        this.polygonTemplate.events.on("hit", (event) => {
            this.onHit(event);
        });
        this.series.exclude = ["AQ"];
    }
    onHit(event) {
        this.articleDiv.innerHTML = "";
        this.articleDiv.remove();
        this.map.zoomToMapObject(event.target);
        let coords = this.map.svgPointToGeo(event.svgPoint);
        GeoCode(coords.latitude, coords.longitude).then(getCountryData).then((country) => this.showCountry(country));

    }
    /**
     * 
     * @param {Country} country
     */
    showCountry(country) {
        this.country = country;
        let exitButton = document.createElement("button");
        exitButton.innerText = "X";
        exitButton.setAttribute("onclick", "closeArticle()");
        this.articleDiv.appendChild(exitButton);
        if (CommentApi.GetUserID() != 0) {
            let editButton = document.createElement("button");
            editButton.innerText = "ערוך";
            editButton.setAttribute("onclick", "PopUpEditWindow()");
            this.articleDiv.appendChild(editButton);
        }
        if (!country.exists) {
            let text = document.createElement("div");
            text.innerHTML = "<h1>על המדינה הזאת אין לנו מידע</h1><br>אולי תוסיף?";
            this.articleDiv.appendChild(text);
        }
        else {
            this.articleDiv.innerHTML += `<h1>${country.CountryName}</h1>`;
            if (country.OfficialArticle && country.OfficialArticle != null) {
                this.articleDiv.innerHTML += "<h2>ערך רשמי</h2>";
                this.articleDiv.innerHTML += country.OfficialArticle;
            }
            if (country.UserArticle && country.UserArticle != null) {
                this.articleDiv.innerHTML += "<h2>ערך של משתמשי האתר</h2>";
                this.articleDiv.innerHTML += country.UserArticle;
            }
            this.MakeCommentBox();
            this.ShowComments();
        }
        this.shadowRoot.appendChild(this.articleDiv);


    }
    //#region comments
    ShowComments() {
        CommentApi.GetCountryComments(this.country.ID).then(comments => {
            //clearing
            this.commentDiv.innerHTML = "";
            this.commentDiv.remove();
            if (comments.length == 0) {
                console.log("here");
                this.commentDiv.innerHTML += "<h3>אין תגובות</h3>";
                return;
            }
            let list = document.createElement("ul");
            for (var comment of comments) {
                let item = document.createElement("li");
                item.innerHTML = `<h5>${comment.UserName}:</h5><br>${comment.Body}<br>`;
                list.appendChild(item);
            }
            this.commentDiv.appendChild(list);
            this.articleDiv.appendChild(this.commentDiv);
        })
    }
    MakeCommentBox() {
        if (CommentApi.GetUserID() == 0) return;
        this.CommentArea = document.createElement("div");
        this.CommentArea.innerHTML +=
            `
        <h3>הוסף תגובה</h3><br>
        <textarea rows="5" cols="50"></textarea>
        <button onclick="SendMapComment()">שלח תגובה</button>
        `;
        this.articleDiv.appendChild(this.CommentArea);
    }
    SendComment() {
        CommentApi.
            BuildAndAddComment(this.country.ID, CommentApi.GetUserID(), this.CommentArea.querySelector("textarea").value)
            .then(() => {
                this.CommentArea.querySelector("textarea").value = "";
                this.ShowComments();
            });
    }
    //#endregion
    //#region edit
    PopUpEditWindow() {
        this.editWindow = document.createElement("div");
        this.editWindow.className = "popup";
        this.editWindow.dir = "ltr";

        let exitButton = document.createElement("button");
        exitButton.innerText = "X";
        exitButton.setAttribute("onclick", "closeEdit()");
        exitButton.style.position = "relative";
        this.editWindow.appendChild(exitButton);
        this.editWindow.innerHTML += "<br>";
        let country = this.country;
        this.textbox = document.createElement("textarea");
        this.TitleBox = document.createElement("textarea");
        this.TitleBox.rows = 1;
        this.TitleBox.cols = 20;
        if (country.exists) {
            this.textbox.value = country.UserArticle;
        }
        else {
            console.log("this happened");
            this.editWindow.appendChild(this.TitleBox);
            this.editWindow.appendChild(document.createElement("br"));
        }
        this.textbox.rows = 40;
        this.textbox.cols = 100;
        this.textbox.dir = "rtl";
        let submitButton = document.createElement("button");
        submitButton.setAttribute("onclick", "SubmitArticleChange()");
        submitButton.innerText = "שלח שינוי";
        this.editWindow.appendChild(this.textbox);
        this.editWindow.appendChild(submitButton);
        this.articleDiv.appendChild(this.editWindow);
    }
    //#endregion
    /**
     * 
     * @param {Country} country
     * @returns {String} 
     */
    /*countryToText(country) {
        return (country.exists ? JSON.stringify(country) : `${country.code} has no data in the database`);
    }*/
    SubmitArticleChange() {
        console.log(this.textbox.value);
        this.country.UserArticle = this.textbox.value;
        console.log(this.TitleBox.value);
        if (this.TitleBox) {
            this.country.CountryName = this.TitleBox.value;
        }
        setCountryData(this.country.code, this.country).then(() => {

            this.editWindow.innerHTML = "";
            this.editWindow.remove();

            this.articleDiv.innerHTML = "";
            this.articleDiv.remove();
            this.showCountry(this.country);
        })
    }
}
function SubmitArticleChange() {
    /**
     * @type {MapElement}
     */
    let map = document.getElementById("map");
    map.SubmitArticleChange();
}
window.SubmitArticleChange = SubmitArticleChange;
function closeArticle() {
    let map = document.getElementById("map");
    map.articleDiv.innerHTML = "";
    map.articleDiv.remove();
}
window.closeArticle = closeArticle;
function closeEdit() {
    let map = document.getElementById("map");
    map.editWindow.innerHTML = "";
    map.editWindow.remove();
}
window.closeEdit = closeEdit;
function SendMapComment() {
    let map = document.getElementById("map");
    map.SendComment();
}
window.SendMapComment = SendMapComment;
function PopUpEditWindow() {
    let map = document.getElementById("map");
    map.PopUpEditWindow();
}
window.PopUpEditWindow = PopUpEditWindow;
window.customElements.define("g-map", MapElement);