﻿class UserHeader extends React.Component {
    render() {
        return (
            <ul className="nav navbar-nav">
                <li><a href="/Home/Index">Главная</a></li>
            </ul> 
        )
    }
}

ReactDOM.render(<UserHeader />, document.getElementById('MasterNavbarMenu'))