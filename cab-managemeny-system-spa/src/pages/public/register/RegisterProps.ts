import IAuthenticationRepository from "../../../modules/user/repositories/IAuthenticationRepository"
import ISessionRepository from "../../../modules/user/repositories/ISessionRepository";

type RegisterProps = {
    repository: IAuthenticationRepository;
    sessionRepository: ISessionRepository;
}

export default RegisterProps;