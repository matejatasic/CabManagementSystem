import IAuthenticationRepository from "../../../modules/user/repositories/IAuthenticationRepository"
import ISessionRepository from "../../../modules/user/repositories/ISessionRepository";

type LoginProps = {
    repository: IAuthenticationRepository;
    sessionRepository: ISessionRepository
}

export default LoginProps;