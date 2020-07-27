import { Component, Input} from '@angular/core';
import Address from '../shared/models/Address';

@Component({
  selector: 'app-address-detail',
  templateUrl: './address-detail.component.html',
  styleUrls: ['./address-detail.component.css']
})
export class AddressDetailComponent {

  countryIsoCodes = ["PL", "FR", "CH", "DE", "IT", "CZ"]

  @Input() address: Address; // TODO: Pozwolić użytkownikowi wybrać z listy krajów

}
