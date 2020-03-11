import React, { Component } from "react";

class userLatNavBar extends Component {
  state = {};
  render() {
    return (
      <span class="border">
        <ul class="nav flex-column">
          <li class="nav-item">
            <a class="nav-link active" href="#">
              Active
            </a>
          </li>
          <li class="nav-item">
            <a class="nav-link" href="#">
              Link
            </a>
          </li>
          <li class="nav-item">
            <a class="nav-link" href="#">
              Link
            </a>
          </li>
          <li class="nav-item">
            <a class="nav-link disabled" href="#">
              Disabled
            </a>
          </li>
        </ul>
      </span>
    );
  }
}

export default userLatNavBar;
