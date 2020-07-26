import Address from './Address';
import Skill from './Skill';

export default class WorkerProfile {
  id: number;
  name: string;
  address: Address;
  skills: Array<Skill>;
}
