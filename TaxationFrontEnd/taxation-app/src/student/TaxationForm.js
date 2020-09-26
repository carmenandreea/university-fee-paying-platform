import React from 'react';
import ReCAPTCHA from 'react-google-recaptcha';
import './index.css';
import './student.css';
import { readUser, readOptions } from './index.js';
let ct = [2, 3, 4];
export class TaxationForm extends React.Component {
  constructor(props) {
    super(props);
    let userDetails = readUser();
    let options = readOptions();
    if (options.length === 0) options = ct;
    console.log(userDetails.address);
    this.state = {
      faculty: userDetails.faculty === undefined ? -1 : userDetails.faculty,
      possibleTaxValues: userDetails.faculty === undefined ? [] : options,
      taxOption: userDetails.taxOption === undefined ? null : userDetails.taxOption,
      firstName: userDetails.firstName === undefined ? null : userDetails.firstName,
      lastName: userDetails.lastName === undefined ? null : userDetails.lastName,
      cnp: userDetails.cnp === undefined ? null : userDetails.cnp,
      email: userDetails.email === undefined ? null : userDetails.email,
      address: userDetails.address === undefined ? null : userDetails.address,
      phone: userDetails.phone === undefined ? null : userDetails.phone,
      country: userDetails.country === undefined ? null : userDetails.country,
      city: userDetails.city === undefined ? null : userDetails.city,
    };
  }
  render() {
    const possibleTaxValues = this.state.possibleTaxValues;
    console.log(possibleTaxValues);
    let count = -2;
    return (
      <div>
        <div class="pcontainer">
        <div class="nav-wrapper">
            <div class="left-side">
                <div class="brand">
                    <div>Universitatea</div>
                </div>
            </div>
            <div class="right-side">
                <div class="nav-button-wrapper">
                    <a href="index.html">Plata</a>
                </div>
                <div class="nav-button-wrapper">
                    <a href="about.html">Universitate</a>
                </div>
                <div class="nav-button-wrapper">
                    <a href="about.html">Studii</a>
                </div>
                <div class="nav-button-wrapper">
                    <a href="./login.html" class="button">Login</a>
                </div>
            </div>
        </div>
    </div>

      <section className="container">
        <div id="div-container">
          <form id="Form" name="form" onSubmit={this._onClickRedirect}>
            <div className="section">
              <p>
                <label className="label"> Facultatea </label>
                <select
                  name="Faculty"
                  id="faculties"
                  form="Form"
                  defaultValue={this.state.faculty !== undefined ? this.state.faculty : '-1'}
                  onChange={this.onChangeUpdate.bind(this)}
                >
                  <option key="-1" value="-1">
                    {' '}
                    Alege opțiunea{' '}
                  </option>
                  <option value="0"> Facultatea de Administrație și Afaceri </option>
                  <option key="1" value="1">
                    {' '}
                    Biologie/ Biology
                  </option>
                  <option key="2" value="2">
                    {' '}
                    Chimie/ Chemestry{' '}
                  </option>
                  <option key="3" value="3">
                    {' '}
                    Drept/ Law{' '}
                  </option>
                  <option key="4" value="4">
                    {' '}
                    Filosofie/ Philosophy{' '}
                  </option>
                  <option key="5" value="5">
                    {' '}
                    Fizică/ Physics{' '}
                  </option>
                  <option key="6" value="6">
                    {' '}
                    Geografie/ Geography{' '}
                  </option>
                  <option key="7" value="7">
                    {' '}
                    Geologie și Geofizică/ Geology amd Geophysics
                  </option>
                  <option key="8" value="8">
                    {' '}
                    Istorie/ History{' '}
                  </option>
                  <option key="9" value="9">
                    {' '}
                    Jurnalism și Științele Comunicării/ Jurnalism and Communication Studies{' '}
                  </option>
                  <option key="10" value="10">
                    {' '}
                    Litere/ Letters{' '}
                  </option>
                  <option>
                    {' '}
                    Facultatea de Limbi și Literaturi Străine/ Foreign Languages and Literatures{' '}
                  </option>
                  <option key="11" value="11">
                    {' '}
                    Matematică și Informatică/ Mathematics and Computer Science
                  </option>
                  <option key="12" value="12">
                    {' '}
                    Sociologie și Asistență Socială/ Sociology and Social Work{' '}
                  </option>
                  <option key="13" value="13">
                    {' '}
                    Psihologie și Științele Educației/ Psychology and Educational Sciences
                  </option>
                  <option key="14" value="14">
                    {' '}
                    Științe Politice/ Political Science
                  </option>
                  <option key="15" value="15">
                    {' '}
                    Teologie Romano-Catolic[/ Romano-Catholic Theology
                  </option>
                  <option key="16" value="16">
                    {' '}
                    Teologie Baptistă/ Baptist Theology{' '}
                  </option>
                  <option key="17" value="17">
                    {' '}
                    Teologie Ortodoxă/ Orthodox Theology{' '}
                  </option>
                </select>
              </p>
              <p>
                <label className="label"> Taxa </label>

                <select
                  name="taxation"
                  id="taxation"
                  autoFocus="autofocus"
                  defaultValue={this.state.taxOption === null ? 0 : this.state.taxOption}
                >
                  <option key="0" value="0">
                    {' '}
                    Selectează ceva{' '}
                  </option>
                  {possibleTaxValues.map(tax => {
                    count += 1;
                    return (
                      <option value={count} key={count}>
                        {tax}
                      </option>
                    );
                  })}
                </select>
              </p>
            </div>
            <div className="section">
              <p>
                <label className="label"> Numele </label>
                <input
                  type="text"
                  name="nume"
                  placeholder={
                    this.state.firstName !== null ? this.state.firstName : 'Scrie numele aici...'
                  }
                  required
                />
              </p>

              <p>
                <label className="label"> Prenumele </label>
                <input
                  type="text"
                  name="fnume"
                  placeholder={
                    this.state.lastName !== null ? this.state.lastName : 'Scrie prenumele aici...'
                  }
                  required
                />
              </p>
              <p>
                <label className="label"> CNP </label>
                <input
                  type="text"
                  name="cnp"
                  placeholder={this.state.cnp !== null ? this.state.cnp : 'Scrie CNP-ul aici...'}
                  required
                />
              </p>
            </div>
            <div className="section">
              <p>
                <label className="label"> E-mail </label>
                <input
                  type="text"
                  name="email"
                  placeholder={
                    this.state.email !== null ? this.state.email : 'Scrie e-mailul aici...'
                  }
                />
              </p>

              <p>
                <label className="label"> Adresa </label>
                <input
                  type="text"
                  name="address"
                  placeholder={
                    this.state.address !== null ? this.state.address : 'Scrie adresa aici...'
                  }
                />
              </p>

              <p>
                <label className="label"> Telefon </label>
                <input
                  type="text"
                  name="telephone"
                  placeholder={
                    this.state.phone !== null ? this.state.phone : 'Scrie telefonul aici...'
                  }
                />
              </p>

              <p>
                <label className="label"> Țara </label>
                <input
                  type="text"
                  name="country"
                  placeholder={
                    this.state.country !== null ? this.state.country : 'Scrie țara aici...'
                  }
                  required
                />
              </p>

              <p>
                <label className="label"> Oraș </label>
                <input
                  type="text"
                  name="city"
                  placeholder={this.state.city !== null ? this.state.city : 'Scrie orașul aici...'}
                />
              </p>
              <ReCAPTCHA
                sitekey="6LetCLkZAAAAALNl3F6XZVPXx_yeWYX072isdqFD"
                onChange={this.onChange}
              />
              <br />
            </div>
            <button type="submit" className="button" id="euplatesc" name="euplatesc">
              {' '}
              Plătește online{' '}
            </button>
          </form>
        </div>
          
        <footer>
          <p> UB | Bd. M. Kogălniceanu 36-46, Sector 5, 050107 | București, ROMANIA | +40-21-307 73 00 | Fax: +40-21-313 17 60 </p>
        </footer>
      </section>



      </div>
    );
  }
  _onClickRedirect(event) {
    event.preventDefault();
    const data = new FormData(event.target);
    const options = Object.fromEntries(data);
    console.log(options);
    /* TODO: SEND TO BACKEND AND MOVE TO DIFFERENT PAGE*/
  }
  onChangeUpdate(event) {
    event.preventDefault();
    console.log(event.target.value);
    /*
    TODO: UPDATE FROM BACKEND
     */
    ct[2] += 1;
    this.setState({ possibleTaxValues: ct });
  }
  onChange(value) {
    console.log('Captcha value:', value);
  }
}
