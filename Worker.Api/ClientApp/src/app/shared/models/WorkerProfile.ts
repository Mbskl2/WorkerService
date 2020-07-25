import Address from './Address';
import Skill from './Skill';

export default class WorkerProfile {
  workerProfileId: number;
  name: string;
  address: Address;
  skills: Array<Skill>;
}
