<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="homepage.aspx.cs" Inherits="LibraryManagement.homepage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style>
        .header__container {
            max-width: var(--max-width);
            margin: auto;
            padding: 2rem 1rem;
            display: grid;
            gap: 2rem;
            text-align: center;
        }

            .header__container h1 {
                margin-bottom: 1rem;
                font-size: 4rem;
                font-weight: 700;
                line-height: 5rem;
                color: var(--text-dark);
            }

                .header__container h1 span {
                    color: var(--primary-color);
                }

            .header__container p {
                margin-bottom: 2rem;
                font-size: 1rem;
                font-weight: 500;
                line-height: 1.75rem;
            }

            .header__container form {
                display: flex;
                align-items: center;
                justify-content: space-between;
                flex-direction: column;
                gap: 1rem 0;
                background-color: #f6f4f7;
                border-radius: 10px;
            }

            .header__container .input__row {
                padding: 1rem;
                display: flex;
                align-items: center;
                gap: 1rem;
                flex: 1;
            }

            .header__container .input__group {
                display: flex;
                align-items: center;
                gap: 10px;
            }

                .header__container .input__group span {
                    color: var(--text-dark);
                }

            .header__container input {
                width: 100%;
                outline: none;
                border: none;
                font-size: 1rem;
                background-color: transparent;
            }

            .header__container button {
                width: 100%;
                padding: 1rem 2rem;
                outline: none;
                border: none;
                font-size: 1rem;
                white-space: nowrap;
                color: var(--white);
                background-color: var(--primary-color);
                border-radius: 10px;
                cursor: pointer;
            }

        @media (width > 768px) {

            .header__container {
                grid-template-columns: repeat(2, 1fr);
                align-items: center;
                text-align: left;
            }

            .header__image {
                grid-area: 1/2/2/3;
            }
        }

        @media (width > 1024px) {
            .header__container form {
                flex-direction: row;
            }

            .header__container button {
                width: fit-content;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <header class="header__container">
        <div class="header__image">
            <img class="home-image" src="Images/homimg.jpg" alt="img" />
        </div>
        <div class="header__content">
            <h1>Single Platform For All Your <span style="color: #6eb0ec">Learning</span> Needs.</h1>
            <p>
                Whether you're seeking to enhance your professional skills, explore
          new hobbies, or pursue academic interests, our comprehensive platform
          offers diverse books and resources tailored to support your lifelong
          learning journey.
            </p>


        </div>
    </header>
    <div class="container">

        <div class="destination__container">

            <div class="socials">
                <span><i class="ri-twitter-x-fill"></i></span>
                <span><i class="ri-facebook-fill"></i></span>
                <span><i class="ri-instagram-line"></i></span>
                <span><i class="ri-youtube-fill"></i></span>
            </div>
            <div class="content">
                <h1>LIBRARY<br />
                    MANAGEMENT<br />
                    <span>SYSTEM</span></h1>
                <p>
                    The E-Library Management System is a web-based platform designed to manage and provide access to a wide range of digital library resources. It allows users to search, borrow, and manage e-books and other digital content efficiently, streamlining library operations and improving user experience.
                </p>
                <asp:Button CssClass="btn" ID="Button1" runat="server" Text="VIEW BOOKS" OnClick="Button1_Click" />
            </div>
            <div class="destination__grid">
                <div class="destination__card">
                    <img src="Images/bis.png" alt="bis" />
                    <div class="cardcontent">
                        <h4>Digital Book Inventory</h4>
                        <p>
                            Explore our extensive digital book inventory, offering a wide range of titles across all genres and fields.
                        </p>

                    </div>
                </div>
                <div class="destination__card">
                    <img src="Images/signup.jpg" alt="signup" />
                    <div class="cardcontent">
                        <h4>Sign Up</h4>
                        <p>
                            Sign up today to access our eLibrary's full range of digital books, resources, and personalized features!
                        </p>

                    </div>
                </div>
                <div class="destination__card">
                    <img src="Images/searchbooks.jpg" alt="searchbooks" />
                    <div class="cardcontent">
                        <h4>Search Books</h4>
                        <p>
                            Search for books easily in our eLibrary. Explore a vast collection across various genres and subjects.
                        </p>

                    </div>
                </div>
            </div>
        </div>
    </div>
    <script>
        const scrollRevealOption = {
            distance: "50px",
            origin: "bottom",
            duration: 1000,
        };

        ScrollReveal().reveal(".header__image img", {
            ...scrollRevealOption,
            origin: "right",
        });
        ScrollReveal().reveal(".header__content h1", {
            ...scrollRevealOption,
            delay: 500,
        });
        ScrollReveal().reveal(".header__content p", {
            ...scrollRevealOption,
            delay: 800,
        });
        ScrollReveal().reveal(".header__content form", {
            ...scrollRevealOption,
            delay: 1200,
        });
        ScrollReveal().reveal(".destination__container", {
            ...scrollRevealOption,
            delay: 1500,
        });
        ScrollReveal().reveal(".destination__grid", {
            ...scrollRevealOption,
            delay: 1400,
        });
    </script>

</asp:Content>
